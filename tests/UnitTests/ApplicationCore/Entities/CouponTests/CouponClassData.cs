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
            yield return new object[] { int.MaxValue, "", int.MaxValue, DateTime.Now.Date.AddDays(1000), DateTime.Now.Date.AddDays(1) };
            yield return new object[] { int.MaxValue, String.Empty, int.MaxValue, DateTime.Now, DateTime.Now.Date.Subtract(TimeSpan.Zero) };
            yield return new object[] { int.MaxValue, String.Empty, int.MaxValue, DateTime.Now, DateTime.Now.Date.Subtract(TimeSpan.FromDays(1)) };
            yield return new object[] { int.MaxValue, String.Empty, int.MaxValue, DateTime.Now, DateTime.Now.Date.Subtract(TimeSpan.FromDays(1000)) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
