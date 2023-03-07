using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CouponSpecification : Specification<Coupon>
{
    public CouponSpecification(params string[] names)
    {
        Query.Where(c => names.Contains(c.Name));
    }
}
