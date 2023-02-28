using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;

public class AddCouponToDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository;

    public AddCouponToDb()
    {
        var coupon = new List<Coupon> { new Coupon { 1,"test1",20,DateTime.Now,DateTime.Now.Date.AddDays((2)) },  new Coupon { 2,"test2",21,DateTime.Now,DateTime.Now.Date.AddDays((3)) }};
        _mockCouponRepository = new Mock<IRepository<List<Coupon>>>();
        _mockCouponRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<CouponWithItemsSpecification>())
            .ReturnsAsync(coupon));
    }
    
    [Fact]
    public async void AddOneCouponSuccess()
    {
        var couponId = 1;
        var coupenName = "spar20";
        var percentageDiscount = 20;
        var startDate = DateTime.Now;
        var endDate = DateTime.Now.Date.AddDays(10);
        var couponService = new CouponService(_mockCouponRepository.Object);
        await couponService.AddCouponToDb(couponId,coupenName,percentageDiscount,startDate,endDate);

        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponWithItemsSpecification>(), default), Times.Once);
    }
    
    public static IEnumerable<object[]> DataFailure =>
        new List<object[]>
        {
            new object[] {1, "test1", 1, DateTime.Now, DateTime.Now.Date.AddDays(-1) },
            new object[] {1, "test2", 1, DateTime.Now.Date.AddDays(1), DateTime.Now },
            new object[] {1, "", 1, DateTime.Now, DateTime.Now.Date.AddDays(1) },
        };
    
    [Theory,MemberData(nameof(DataFailure),parameters:5)]
    public async void AddOneCouponFailure(int couponId, string couponName,int percentageDiscount, DateTime startDate, DateTime endDate)
    {
        var couponService = new CouponService(_mockCouponRepository.Object);
      
        Assert.Throws<ArgumentException>(() =>
        couponService.AddCouponToDb(couponId, couponName, percentageDiscount, startDate, endDate).Result
        );
        //_mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponWithItemsSpecification>(), default), Times.Once);
    }
}
