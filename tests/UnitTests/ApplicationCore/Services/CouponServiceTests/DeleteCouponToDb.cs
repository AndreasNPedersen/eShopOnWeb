using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;

public class DeleteCouponToDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository = new();
    private Coupon coupon = new ();

    public DeleteCouponToDb()
    {
        coupon = new Coupon(1, "test1", 20, DateTime.Now, DateTime.Now.Date.AddDays(2));
        _mockCouponRepository = new Mock<IRepository<Coupon>>();
        _ = _mockCouponRepository.Setup(x => x.GetByIdAsync(It.IsAny<CouponByIdSpec>(), default)).ReturnsAsync(coupon);
    }

    [Fact]
    public async void DeleteOneCouponSuccess()
    {
        var couponId = 1;
        var couponService = new CouponService(_mockCouponRepository.Object);
        var gottenResponse = await couponService.DeleteCouponFromDb(couponId);

        Assert.True(gottenResponse);
        _mockCouponRepository.Verify(x => x.GetByIdAsync(It.IsAny<CouponByIdSpec>(), default), Times.Once);
        
    }

    [Fact]
    public async void DeleteOneCouponFailure()
    {
        var couponId = 3;
        _mockCouponRepository.Reset();

        var couponService = new CouponService(_mockCouponRepository.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>await  couponService.DeleteCouponFromDb(couponId));

    }
}
