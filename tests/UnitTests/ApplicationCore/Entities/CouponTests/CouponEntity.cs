using Xunit;
using Microsoft.eShopWeb.ApplicationCore.Entities;
namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.CouponTests;

public class CouponEntity
{
    [Theory, ClassData(typeof(CouponClassData))]
    public void AddCouponEntrySuccess(int id, string name, int percentageDiscount,DateTime startDate, DateTime endDate)
    {   
        //Arrange
        
        //Act
        var coupon = new Coupon(id,name,percentageDiscount,startDate,endDate);
        
        //Assert
        Assert.Equal(id,coupon.Id);
        Assert.Equal(name,coupon.Name);
        Assert.Equal(percentageDiscount,coupon.PercentageDiscount);
        Assert.Equal(startDate,coupon.StartDate);
        Assert.Equal(endDate,coupon.EndDate);
    }
    
    public static IEnumerable<object[]> DataFailure =>
        new List<object[]>
        {
            new object[] { int.MaxValue, int.MaxValue, "", DateTime.Now},
            new object[] {int.MinValue,int.MinValue,"", DateTime.Now },
            new object[] {int.MaxValue,int.MinValue,"", DateTime.Now },
            new object[] {int.MinValue,int.MaxValue,String.Empty, DateTime.Now, },
        };
    
    [Theory, MemberData(nameof(DataFailure), parameters: 4)]
    public void AddCouponEntryFails(int id, int percentageDiscount, string couponCode,DateTime todayDate)
    {   
        // Testing DateTime values, since the Compiler is clever enough to know when an int is too large,
        // cannot test DateTime add or subtract in memberdata.
        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(()=> 
            new Coupon(id, couponCode, percentageDiscount, todayDate, DateTime.Now.Date.AddDays(int.MaxValue)));
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Coupon(id, couponCode, percentageDiscount, todayDate, DateTime.Now.Date.AddDays(int.MinValue)));
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Coupon(id, couponCode, percentageDiscount, DateTime.Now.Date.AddDays(int.MaxValue), todayDate));
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Coupon(id, couponCode, percentageDiscount, DateTime.Now.Date.AddDays(int.MinValue), todayDate));
    }
    
}
