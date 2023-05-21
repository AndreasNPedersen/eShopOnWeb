using System;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;

public class AddCouponRequest : BaseRequest
{
    public string Name { get; set; }
    public int PercentageDiscount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public AddCouponRequest() { }
}
