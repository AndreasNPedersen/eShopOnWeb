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
    private Coupon _coupon;
    private List<Coupon> _couponList;

    public GetCouponFromDb()
    {
        int id = 1;
        string couponCode = "test1";
        int percentageDiscount = 20;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(2);
        _coupon = new Coupon(id, couponCode, percentageDiscount, startDate, endDate);
        _couponList = new List<Coupon>();
        _couponList.Add(_coupon);

        _mockCouponRepository = new Mock<IRepository<Coupon>>();
    }

    [Fact]
    public async void GetOneCouponSuccess()
    {
        var couponName = "test1";
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default)).ReturnsAsync(_coupon);
        var couponService = new CouponService(_mockCouponRepository.Object);
        Coupon gottenCoupon = await couponService.GetCoupon(couponName);

        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);
        Assert.Equivalent(_coupon, gottenCoupon);
    }

    [Fact]
    public async void GetOneNotInRepCouponFailure()
    {
        string couponToSearch = "test2";
 
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default));
        var couponService = new CouponService(_mockCouponRepository.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await couponService.GetCoupon(couponToSearch));
       
    }


    [Fact]
    public async void GetCouponsSuccess()
    {
        _ = _mockCouponRepository.Setup(x => x.ListAsync(default)).ReturnsAsync(_couponList);
        var couponService = new CouponService(_mockCouponRepository.Object);

        var coupons = await couponService.GetCoupons();

        _mockCouponRepository.Verify(x => x.ListAsync(default), Times.Once);
        Assert.Equivalent(_couponList, coupons);
    }
}
