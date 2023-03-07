using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;
public class Coupon : BaseEntity, IAggregateRoot
{
    //public Coupon() { }
    public Coupon(int id, string name, int percentageDiscount, DateTime startDate, DateTime endDate)
    {
        this.Id = id;
        this.Name = name;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.PercentageDiscount = percentageDiscount;
    }
    //public int Id { get; set; }
    public string Name { get; set; }
    public int PercentageDiscount {  get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
