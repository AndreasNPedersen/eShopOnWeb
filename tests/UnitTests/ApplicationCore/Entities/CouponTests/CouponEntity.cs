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
            new object[] { "", DateTime.Now},
            new object[] {"", DateTime.Now },

        };
    
    [Theory, MemberData(nameof(DataFailure), parameters: 2)]
    public void AddCouponEntryFails(string name,DateTime todayDate)
    {   
        // Testing DateTime values, since the Compiler is clever enough to know when an int is too large, cannot test int.
        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(()=> new Coupon(int.MaxValue,name, int.MaxValue, todayDate, DateTime.Now.Date.AddDays(int.MaxValue)));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coupon(int.MaxValue, name, int.MaxValue, todayDate, DateTime.Now.Date.AddDays(int.MinValue)));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coupon(int.MaxValue, name, int.MaxValue, DateTime.Now.Date.AddDays(int.MaxValue), todayDate));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coupon(int.MaxValue, name, int.MaxValue, DateTime.Now.Date.AddDays(int.MinValue), todayDate));
    }
    
}
