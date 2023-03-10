using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;
public class CouponSpecificationTest
{


    [Theory]
    [InlineData(1, "asd")]
    [InlineData(0, "kpo")]
    [InlineData(1,"test0")]
    public void MatchesExpectedNumberOfItems(int expectedCount, string couponName)
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CouponSpecification(couponName);

        var result = spec.Evaluate(GetTestItemCollection()).ToList();

        Assert.Equal(expectedCount, result.Count());
    }

    public List<Coupon> GetTestItemCollection()
    {
        return new List<Coupon>()
            {
                new(0,"test0",12,DateTime.Now,DateTime.Now.AddDays(2)),
                new(1,"test1",12,DateTime.Now,DateTime.Now.AddDays(2)),
                new(2,"asd",12,DateTime.Now,DateTime.Now.AddDays(2))
            };
    }
}
