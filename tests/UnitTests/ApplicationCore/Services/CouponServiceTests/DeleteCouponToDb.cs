using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CouponServiceTests;

public class DeleteCouponToDb
{
    private readonly Mock<IRepository<Coupon>> _mockCouponRepository;

    public DeleteCouponToDb()
    {
        var coupon = new List<Coupon> { new Coupon { 1,"test1",20,DateTime.Now,DateTime.Now.Date.AddDays((2)) },  new Coupon { 2,"test2",21,DateTime.Now,DateTime.Now.Date.AddDays((3)) }};
        _mockCouponRepository = new Mock<IRepository<List<Coupon>>>();
        _mockCouponRepository.Setup(x =>  x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(coupon);
    }
    
    [Fact]
    public async void DeleteOneCouponSuccess()
    {
        var couponId = 1;
        var couponService = new CouponService(_mockCouponRepository.Object);
        await couponService.DeleteCouponFromDb(couponId);

        _mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponWithItemsSpecification>(), default), Times.Once);
    }
    
    [Fact]
    public async void DeleteOneCouponFailure()
    {
        var couponId = 3;
        var couponService = new CouponService(_mockCouponRepository.Object);
        Assert.Throws<ArgumentNullException>(()=>  couponService.DeleteCouponFromDb(couponId).Result);
        //_mockCouponRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<CouponWithItemsSpecification>(), default), Times.Once);
    }
}
