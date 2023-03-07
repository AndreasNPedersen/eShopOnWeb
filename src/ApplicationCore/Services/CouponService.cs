using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class CouponService : ICouponService
{
    
    private readonly IRepository<Coupon> _couponRepository;

    public CouponService(IRepository<Coupon> couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<List<Coupon>> GetCoupons()
    {
        return await _couponRepository.ListAsync();
    }
    
    public async Task<Coupon> GetCoupon(string couponName)
    {
        var couponSpec = new CouponSpecification(couponName);
        var coupon = await _couponRepository.FirstOrDefaultAsync(couponSpec);
        Guard.Against.Null(coupon, nameof(coupon));
        return coupon;
    }
    
    
    public async Task<bool> AddCouponToDb(string couponName, int percentageDiscount, DateTime startDate, DateTime endDate)
    {
        var couponSpec = new CouponSpecification(couponName);
        var newCoupon = await _couponRepository.FirstOrDefaultAsync(couponSpec);

        if (newCoupon == null)
        {
            return false;
        }

        _couponRepository.AddAsync(new Coupon()
        {
            Name = couponName, PercentageDiscount = percentageDiscount, StartDate = startDate, EndDate = endDate
        });
        return true;

    }

    public async Task<bool> DeleteCouponFromDb(int couponId)
    {
        var coupon = await _couponRepository.GetByIdAsync(couponId);
        Guard.Against.Null(coupon, nameof(coupon));
        await _couponRepository.DeleteAsync(coupon);
    }
}
