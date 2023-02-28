using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.CouponsTests;

public class GetCoupon
{
    private readonly Mock<IReadRepository<Coupon>> _mockCouponRepository;

    public GetCoupon()
    {
        var coupon = new List<Coupon> { new Coupon { 1,"test1",20,DateTime.Now,DateTime.Now.Date.AddDays((2)) },  new Coupon { 2,"test2",21,DateTime.Now,DateTime.Now.Date.AddDays((3)) }};
        _mockCouponRepository = new Mock<IReadRepository<List<Coupon>>>();
        _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Coupon>())
            .ReturnsAsync(coupon));
    }
    
    [Fact]
    public async void CheckOneCouponSuccess()
    {
        var couponId = 1;
        
        var request = new eShopWeb.Web.Features.Coupons.CheckCoupon(couponId);
        
        var handler = new GetCouponHandler(_mockCouponRepository.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
    }
    
    [Fact]
    public async void CheckOneCouponFailure()
    {
        var couponId = 3;
        
        var request = new eShopWeb.Web.Features.Coupons.CheckCoupon(couponId);
        
        var handler = new GetCouponHandler(_mockCouponRepository.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.Null(result);
    }
}
