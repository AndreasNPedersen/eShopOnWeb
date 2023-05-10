using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
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

    public async Task<Coupon> GetCoupon(int couponId)
    {
        var couponSpec = new CouponByIdSpec(couponId);
        var coupon = await _couponRepository.FirstOrDefaultAsync(couponSpec);
        Guard.Against.Null(coupon, nameof(coupon));
        return coupon;
    }


    public async Task<Coupon> GetAndCheckCoupon(string couponCode)
    {
        DateTime today = DateTime.Now;
        Coupon checkedCoupon = await GetCoupon(couponCode);


        if (checkedCoupon != null && checkedCoupon.StartDate < today && checkedCoupon.EndDate > today)
        {
            return checkedCoupon;
        }

        throw new CouponNotValidException(couponCode);
    }


    public async Task<bool> AddCouponToDb(string couponName, int percentageDiscount, DateTime startDate, DateTime endDate)
    {
        var couponSpec = new CouponSpecification(couponName);
        var newCoupon = await _couponRepository.FirstOrDefaultAsync(couponSpec);

        if (newCoupon != null || startDate > endDate )
        {
            return false;
        }

        await _couponRepository.AddAsync(new Coupon()
        {
            Name = couponName, PercentageDiscount = percentageDiscount, StartDate = startDate, EndDate = endDate
        });
        return true;

    }

    public async Task<bool> DeleteCouponFromDb(int couponId)
    {
        var couponSpec = new CouponByIdSpec(couponId);
        var coupon = await _couponRepository.GetByIdAsync(couponSpec);
        Guard.Against.Null(coupon, nameof(coupon));
        await _couponRepository.DeleteAsync(coupon);
        return true;
    }
}
