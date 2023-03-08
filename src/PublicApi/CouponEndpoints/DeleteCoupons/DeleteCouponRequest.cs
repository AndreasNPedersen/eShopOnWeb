namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.DeleteCoupons;

public class DeleteCouponRequest : BaseRequest
{
    public int CouponId { get; init; }

    public DeleteCouponRequest(int couponId)
    {
        CouponId = couponId;
    }
}
