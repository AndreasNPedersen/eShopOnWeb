using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;
public class Coupon : BaseEntity, IAggregateRoot
{
    public Coupon() { }
    public Coupon(int id, string name, int percentageDiscount, DateTime startDate, DateTime endDate)
    {
        base.Id = id;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        PercentageDiscount = percentageDiscount;
    }
    public string Name { get; set; }
    public int PercentageDiscount {  get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
