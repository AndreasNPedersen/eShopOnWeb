using System;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;

public class CouponGetResponse : BaseResponse
{
    public CouponGetResponse(Guid correlationId) : base(correlationId)
    {

    }

    public CouponGetResponse() { }

    public CouponDto Coupon { get; set; }
}
