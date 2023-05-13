using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;

public class GetCouponListResponse : BaseResponse
{
    public GetCouponListResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetCouponListResponse() { }

    public List<Coupon> Coupons { get; set; }
}
