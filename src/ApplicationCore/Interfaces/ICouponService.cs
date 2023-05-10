using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface ICouponService
{
    Task<bool> AddCouponToDb(string couponName,int percentageDiscount, DateTime startDate, DateTime endDate);
    
    Task<bool> DeleteCouponFromDb(int couponId);

    Task<Coupon> GetCoupon(string couponName);

    Task<List<Coupon>> GetCoupons();
    Task<Coupon> GetAndCheckCoupon(string couponCode);
}
