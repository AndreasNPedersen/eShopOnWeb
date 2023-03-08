using System;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.DeleteCoupons;

public class DeleteCouponResponse : BaseResponse
{
    public DeleteCouponResponse(Guid correlationId): base(correlationId)
    {
    }

    public DeleteCouponResponse()
    {
    }

    public bool DeletedStatus { get; set; } = true;
}
