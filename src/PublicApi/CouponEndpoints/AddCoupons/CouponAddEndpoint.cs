using Ardalis.Result;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;

public class CouponAddEndpoint
{
    private readonly IUriComposer _uriComposer;

    public CouponAddEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/addCoupon",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (AddCouponRequest request, IRepository<Coupon> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<AddCouponResponse>()
            .WithTags("CouponEndpoints");
    }

    public async Task<AspNetCore.Http.IResult> HandleAsync(AddCouponRequest request, IRepository<Coupon> itemRepository)
    {
        var response = new AddCouponResponse(request.CorrelationId());

        var couponNameSpecification = new CouponSpecification(request.Name);
        var existingCataloogItem = await itemRepository.FirstOrDefaultAsync(couponNameSpecification);
        if (existingCataloogItem == null)
        {
            throw new DuplicateException($"A coupon with name {request.Name} already exists");
        }

        var newItem = new Coupon();
        newItem.Name = request.Name;
        newItem.PercentageDiscount = request.PercentageDiscount;
        newItem.StartDate = request.StartDate;
        newItem.EndDate = request.EndDate;

        await itemRepository.AddAsync(newItem);
        response.CreatedCoupon = true;

        return Results.Created($"api/addCoupon/", response);
    }

}
