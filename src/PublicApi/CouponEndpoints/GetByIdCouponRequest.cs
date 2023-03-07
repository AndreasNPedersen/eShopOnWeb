namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints;

public class GetByIdCouponRequest : BaseRequest
{
    public int CouponId { get; init; }

    public GetByIdCouponRequest(int couponId)
    {
        CouponId = couponId;
    }
}
