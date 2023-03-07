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
            new object[] { int.MaxValue, "", int.MaxValue, DateTime.Now, DateTime.Now.Date.AddDays(int.MaxValue)},
            new object[] { int.MinValue, "", int.MinValue, DateTime.Now, DateTime.Now.Date.AddDays(int.MinValue)},
            new object[] {1, "", 1, DateTime.Now, DateTime.Now.Date.AddDays(int.MaxValue) },
            new object[] { 1, "", 1, DateTime.Now, DateTime.Now.Date.AddDays(int.MinValue) },
        };
    
    [Theory, MemberData(nameof(DataFailure), parameters: 5)]
    public void AddCouponEntryFails(int id, string name, int percentageDiscount,DateTime startDate, DateTime endDate)
    {   
        //Arrange
        
        //Act

        //Assert
        Assert.Throws<FormatException>(()=> new Coupon(id,name,percentageDiscount,startDate,endDate));
    }
    
}
