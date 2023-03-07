using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;

public class AddCouponResponse : BaseResponse
{
    public AddCouponResponse(Guid correlationId) : base(correlationId)
    {
    }

    public AddCouponResponse()
    {
    }

    public bool CreatedCoupon { get; set; }
}
