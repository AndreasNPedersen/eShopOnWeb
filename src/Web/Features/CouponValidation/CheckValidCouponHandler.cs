using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Web.Features.CouponValidation;

public class CheckValidCouponHandler
{

    public async static Task<Coupon> GetAndCheckCoupon(ICouponService service, string couponCode)
    {
        DateTime today = DateTime.Now;
        Coupon checkedCoupon = await service.GetCoupon(couponCode);


        if (checkedCoupon != null && checkedCoupon.StartDate < today && checkedCoupon.EndDate > today)
        {
            return checkedCoupon;
        }

        throw new CouponNotValidException(couponCode);
    }
}
