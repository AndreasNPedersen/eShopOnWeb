using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public class CouponByIdSpec : Specification<Coupon>
{
    public CouponByIdSpec(int id)
    {
        Query.Where(c => c.Id == id);
    }
}
