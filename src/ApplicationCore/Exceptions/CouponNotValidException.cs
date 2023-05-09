using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions;
public class CouponNotValidException : Exception
{
    public CouponNotValidException(string couponCode):base($"Coupon code:{couponCode} not valid")
    {
    }

    public CouponNotValidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CouponNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
