using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;
public class GetIDCouponFromDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository = new();
    private Coupon _coupon;

    public GetIDCouponFromDb() {
        int id = 1;
        string couponCode = "test1";
        int percentageDiscount = 20;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(2);
        _coupon = new Coupon(id, couponCode, percentageDiscount, startDate, endDate);

        _mockCouponRepository = new Mock<IRepository<Coupon>>();
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponByIdSpec>(), default)).ReturnsAsync(_coupon);


    }

    [Fact]
    public async void GetOneCouponSuccess()
    {
        var couponId = 1;
        var couponService = new CouponService(_mockCouponRepository.Object);
        var gottenCoupon = await couponService.GetCoupon(couponId);

        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponByIdSpec>(), default), Times.Once);

        Assert.Equivalent(_coupon, gottenCoupon);
    }

    [Fact]
    public async void GetOneCouponFailure()
    {
        int couponToSearch = 2;

        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponByIdSpec>(), default));
        var couponService = new CouponService(_mockCouponRepository.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await couponService.GetCoupon(couponToSearch));

    }

}
