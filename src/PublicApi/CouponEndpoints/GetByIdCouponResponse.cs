using System;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints;

public class GetByIdCouponResponse : BaseResponse
{

    public GetByIdCouponResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdCouponResponse()
    {
    }

    public CouponDto Coupon { get; set; }
}
