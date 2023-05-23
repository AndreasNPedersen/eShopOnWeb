using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;
public class GetAndCheckCouponFromDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository = new();
    private Coupon couponOne = new Coupon(1, "test1", 20, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now.Date.AddDays(2));
    private Coupon couponTwo = new Coupon(2, "test2", 20, DateTime.Now.Subtract(TimeSpan.FromDays(10)), DateTime.Now.Subtract(TimeSpan.FromDays(5)));

    public GetAndCheckCouponFromDb() {
        _mockCouponRepository = new Mock<IRepository<Coupon>>();
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default)).ReturnsAsync(couponOne);
 
    }

    [Fact]
    public async void GetOneCouponSuccess_WhenCouponIsValidDate()
    {
        var couponService = new CouponService(_mockCouponRepository.Object);

        Coupon checkedCoupon = await couponService.GetAndCheckCoupon(couponOne.Name);
        
        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);
        Assert.Equivalent(checkedCoupon, couponOne);
    }

    [Theory]
    [InlineData("test2")]
    public async void GetOneCouponFailure_whenCouponIsNotValidDate(string couponName)
    {
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default)).ReturnsAsync(couponTwo);
        var couponService = new CouponService(_mockCouponRepository.Object);


        await Assert.ThrowsAsync<CouponNotValidException>(async () => await couponService.GetAndCheckCoupon(couponName));
        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);

    }


    [Theory]
    [InlineData("test3")]
    public async void GetOneCouponFailure_whenCouponIsNotValid(string couponName)
    {
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default));
        var couponService = new CouponService(_mockCouponRepository.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await couponService.GetAndCheckCoupon(couponName));
        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);

    }
}
