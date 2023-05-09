using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Features.CouponValidation;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

[Authorize]
public class CheckoutModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IOrderService _orderService;
    private string? _username = null;
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly IAppLogger<CheckoutModel> _logger;
    private readonly ICouponService _couponService;

    public CheckoutModel(IBasketService basketService,
        IBasketViewModelService basketViewModelService,
        SignInManager<ApplicationUser> signInManager,
        IOrderService orderService,
        IAppLogger<CheckoutModel> logger,
        ICouponService couponService)
    {
        _basketService = basketService;
        _signInManager = signInManager;
        _orderService = orderService;
        _basketViewModelService = basketViewModelService;
        _logger = logger;
        _couponService = couponService;
    }

    public BasketViewModel BasketModel { get; set; } = new BasketViewModel();
    [BindProperty]
    public Coupon CouponInput { get; set; }
    [BindProperty]
    public string ErrorForCoupon { get; set; }
    [BindProperty]
    public int PercentageDiscount { get; set; }
    [BindProperty]
    public decimal Total { get; set; }

    public async Task OnGet()
    {
        await SetBasketModelAsync();
        if (Request.Cookies.ContainsKey("Coupon"))
        {
            PercentageDiscount = Convert.ToInt32(Request.Cookies["Coupon"]);
        }
        CalculateTotal();
    }

    public void CalculateTotal()
    {
        if (PercentageDiscount != 0)
        {
           
            decimal basketTotal = BasketModel.Items.Sum(x => x.UnitPrice * x.Quantity);
            decimal basketTotalWithCoupon = (decimal)(PercentageDiscount / 100.00);
            Total = Math.Round(basketTotal - (basketTotal * basketTotalWithCoupon), 2);
        }
        else
        {
            Total = Math.Round(BasketModel.Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }

    }

    public async Task<IActionResult> OnPost(IEnumerable<BasketItemViewModel> items)
    {
        try
        {
            await SetBasketModelAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
            await _basketService.SetQuantities(BasketModel.Id, updateModel);
            await _orderService.CreateOrderAsync(BasketModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240"));
            await _basketService.DeleteBasketAsync(BasketModel.Id);
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append("Coupon", "0", cookieOptions);
        }
        catch (EmptyBasketOnCheckoutException emptyBasketOnCheckoutException)
        {
            //Redirect to Empty Basket page
            _logger.LogWarning(emptyBasketOnCheckoutException.Message);
            return RedirectToPage("/Basket/Index");
        }

        return RedirectToPage("Success");
    }

    private async Task SetBasketModelAsync()
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name);
        }
        else
        {
            GetOrSetBasketCookieAndUserName();
            BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(_username!);
        }
    }

    private void GetOrSetBasketCookieAndUserName()
    {
        if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
        {
            _username = Request.Cookies[Constants.BASKET_COOKIENAME];
        }
        if (_username != null) return;

        _username = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        Response.Cookies.Append(Constants.BASKET_COOKIENAME, _username, cookieOptions);
    }

    public async Task<IActionResult> OnPostHandleCoupon(string couponCode)
    {
        if (!String.IsNullOrWhiteSpace(couponCode) && couponCode.Length > 0)
        {
            try
            {
                Coupon checkCoupon = await CheckValidCouponHandler.GetAndCheckCoupon(_couponService,couponCode);
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Today.AddYears(10);
                Response.Cookies.Append("Coupon", checkCoupon.PercentageDiscount.ToString(), cookieOptions);
            } catch (CouponNotValidException ex)
            {
                ErrorForCoupon = ex.Message; // this would be our error handling frontend if it wasn't server side rendering.
            }
        }
        return RedirectToPage("/Basket/Checkout");
    }
}
