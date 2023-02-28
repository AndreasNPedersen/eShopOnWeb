using System.Collections;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.CouponTests;

public class CouponClassData : IEnumerable<object[]>
    {
        /// <summary>
        /// To pass in class data for the success criteria
        /// </summary>
        /// <returns>The Objects of Coupons</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, "", 20, DateTime.Now, DateTime.Now.Date.AddDays(10)};
            yield return new object[] { int.MinValue, "", int.MinValue, DateTime.Now, DateTime.Now.Date.AddDays(1000)};
            yield return new object[] { int.MaxValue, "", int.MaxValue, DateTime.Now, DateTime.Now.Date.AddDays(1)};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
