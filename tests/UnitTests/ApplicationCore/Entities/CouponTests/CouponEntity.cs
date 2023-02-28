using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.CouponTests;

public class CouponEntity
{

    [Theory, ClassData(typeof(CouponClassData))]
    public void AddCouponEntrySuccess(int id, string couponName, int percentageDiscount,DateTime startDate, DateTime endDate)
    {   
        //Arrange
        
        //Act
        var coupon = new Coupon(id,couponName,percentageDiscount,startDate,endDate);
        
        //Assert
        Assert.Equal(id,coupon.Id);
        Assert.Equal(couponName,coupon.CouponName);
        Assert.Equal(percentageDiscount,coupon.PercentageDiscount);
        Assert.Equal(startDate,coupon.StartDate);
        Assert.Equal(endDate,coupon.EndDate);
    }
    
    public static IEnumerable<object[]> DataFailure =>
        new List<object[]>
        {
            new object[] { int.MaxValue+1, "", int.MaxValue+1, DateTime.Now, DateTime.Now.Date.AddDays(int.MaxValue)},
            new object[] { int.MinValue-1, "", int.MinValue-1, DateTime.Now, DateTime.Now.Date.AddDays(int.MinValue)},
            new object[] {1, "", 1, DateTime.Now, DateTime.Now.Date.AddDays(int.MaxValue+1) },
            new object[] { 1, "", 1, DateTime.Now, DateTime.Now.Date.AddDays(int.MinValue-1) },
        };
    
    [Theory, MemberData(nameof(DataFailure), parameters: 5)]
    public void AddCouponEntryFails(int id, string couponName, int percentageDiscount,DateTime startDate, DateTime endDate)
    {   
        //Arrange
        
        //Act
        var coupon = new Coupon(id,couponName,percentageDiscount,startDate,endDate);
        
        //Assert
        Assert.Equal(id,coupon.Id);
        Assert.Equal(couponName,coupon.CouponName);
        Assert.Equal(percentageDiscount,coupon.PercentageDiscount);
        Assert.Equal(startDate,coupon.StartDate);
        Assert.Equal(endDate,coupon.EndDate);
    }
    
}
