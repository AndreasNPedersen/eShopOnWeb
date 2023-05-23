using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Data.Migrations;
using Moq;
using Xunit;
using Coupon = Microsoft.eShopWeb.ApplicationCore.Entities.Coupon;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;

public class AddCouponToDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository = new();
    private Coupon coupon = new();


    public AddCouponToDb()
    {
        int id = 1;
        string couponCode = "test1";
        int percentageDiscount = 20;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(2);
        coupon = new Coupon(id,couponCode,percentageDiscount,startDate,endDate);
        _mockCouponRepository = new Mock<IRepository<Coupon>>();
       _= _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default)).ReturnsAsync(coupon);
    }

    [Fact]
    public async void AddOneCouponSuccess()
    {
        _ = _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default)); //nothing in the db mocked

        var couponService = new CouponService(_mockCouponRepository.Object);
       var gottenCoupon = await couponService.AddCouponToDb(coupon.Name, coupon.PercentageDiscount, coupon.StartDate, coupon.EndDate);
        _ = _mockCouponRepository.Setup(x => x.AddAsync(coupon, default));

        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);
        _mockCouponRepository.Verify(x => x.AddAsync(It.IsAny<Coupon>(), default), Times.Once);
        Assert.True(gottenCoupon);
    }

    public static IEnumerable<object[]> DataFailure =>
        new List<object[]>
        {
            new object[] {"test1", 1, DateTime.Now, DateTime.Now ,-111111111},
            new object[] {"test1", 1, DateTime.Now, DateTime.Now,100000000 },
            new object[] {"test1", 1, DateTime.Now.AddDays(2), DateTime.Now,100000000 }
        };

    [Theory, MemberData(nameof(DataFailure), parameters: 5)]
    public async void AddOneCouponFailure(string couponName, int percentageDiscount, DateTime startDate, DateTime endDate, int days)
    {
        var couponService = new CouponService(_mockCouponRepository.Object);


        Assert.Throws<ArgumentOutOfRangeException>(() =>
        couponService.AddCouponToDb(couponName, percentageDiscount, startDate.AddDays(days), endDate.AddDays(days)).Result);
    }

    [Fact]
    public async void AddOneAlreadyInDb_ExpectedFailure()
    {
        var couponService = new CouponService(_mockCouponRepository.Object);
        var couponGotten = await couponService.AddCouponToDb(coupon.Name, coupon.PercentageDiscount, coupon.StartDate, coupon.EndDate);
        
        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponSpecification>(), default), Times.Once);
        Assert.False(couponGotten);
    }
}
