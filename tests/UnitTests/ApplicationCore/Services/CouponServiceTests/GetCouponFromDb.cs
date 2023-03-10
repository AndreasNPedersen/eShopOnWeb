using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;
public class GetCouponFromDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository = new();

    public GetCouponFromDb()
    {
        var coupon = new Coupon(1, "test1", 20, DateTime.Now, DateTime.Now.Date.AddDays(2));
        var couponList = new List<Coupon>();
        couponList.Add(coupon);

        _mockCouponRepository = new Mock<IRepository<Coupon>>();
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default)).ReturnsAsync(coupon);
        _ = _mockCouponRepository.Setup(x => x.ListAsync(It.IsAny<CouponSpecification>(),default)).ReturnsAsync(couponList);
    }

    [Fact]
    public async void GetOneCouponSuccess()
    {
        var couponName = "test1";
        var couponService = new CouponService(_mockCouponRepository.Object);
        await couponService.GetCoupon(couponName);

        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);
    }

    [Fact]
    public async void GetOneCouponFailure()
    {
        string couponToSearch = "test2";
 
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default));
        var couponService = new CouponService(_mockCouponRepository.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await couponService.GetCoupon(couponToSearch));
       
    }


    [Fact]
    public async void GetCouponsSuccess()
    {
        var couponService = new CouponService(_mockCouponRepository.Object);
        var coupons = await couponService.GetCoupons();
        _mockCouponRepository.Verify(x => x.ListAsync(default), Times.Once);
    }
}
