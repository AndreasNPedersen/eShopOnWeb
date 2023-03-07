using System;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;

public class Coupon : BaseEntity, IAggregateRoot
{
    public string CouponName { get; set; }
    public int PercentageDiscount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Coupon() { }

    public Coupon(int id, string couponName, int percentageDiscount, DateTime startDate, DateTime endDate)
    {
        base.Id = id;
        CouponName = couponName;
        PercentageDiscount = percentageDiscount;
        StartDate = startDate;
        EndDate = endDate;
    }
}
