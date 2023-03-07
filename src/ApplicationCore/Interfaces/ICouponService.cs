using System;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface ICouponService
{
    Task<bool> AddCouponToDb(string couponName,int percentageDiscount, DateTime startDate, DateTime endDate);
    
    Task<bool> DeleteCouponFromDb(int couponId);
}
