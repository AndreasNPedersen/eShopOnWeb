using System;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints;

public class CouponDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PercentageDiscount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
