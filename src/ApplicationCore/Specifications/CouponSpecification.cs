using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

/// <summary>
/// Specification filtering on a coupons code/name
/// </summary>
public class CouponSpecification : Specification<Coupon>
{
    public CouponSpecification(string couponCode)
    {
        Query.Where(c => c.Name == couponCode);
    }


}
