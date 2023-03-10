using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities;

using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;
public class CouponByIdSpecTest
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    public void MatchesExpectedNumberOfItems(int expectedCount, int couponId)
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CouponByIdSpec(couponId);

        var result = spec.Evaluate(GetTestItemCollection()).ToList();

        Assert.Equal(expectedCount, result.Count());
    }

    public List<Coupon> GetTestItemCollection()
    {
        return new List<Coupon>()
            {
                new(3,"test0",12,DateTime.Now,DateTime.Now.AddDays(2)),
                new(1,"test1",12,DateTime.Now,DateTime.Now.AddDays(2)),
                new(2,"asd",12,DateTime.Now,DateTime.Now.AddDays(2))
            };
    }
}


 
