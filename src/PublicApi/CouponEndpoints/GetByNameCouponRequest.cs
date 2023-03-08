namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints;

public class GetByNameCouponRequest : BaseRequest
{
    public string CouponName { get; init; }

    public GetByNameCouponRequest(string couponName)
    {
        CouponName = couponName;
    }
}
