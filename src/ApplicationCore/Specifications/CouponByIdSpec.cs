using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

/// <summary>
/// Specification filtering on a coupons id
/// </summary>
public class CouponByIdSpec : Specification<Coupon>
{
    public CouponByIdSpec(int id)
    {
        Query.Where(c => c.Id == id);
    }
}
