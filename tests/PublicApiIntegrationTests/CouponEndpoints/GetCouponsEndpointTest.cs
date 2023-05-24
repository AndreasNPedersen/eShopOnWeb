using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Azure.Core;

namespace PublicApiIntegrationTests.CouponEndpoints;
[TestClass]
public class GetCouponsEndpointTest
{
    [TestMethod]
    public async Task testGetCoupons()
    {
        var token = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/coupon");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        List<CouponDto> tmpList = JsonConvert.DeserializeObject<List<CouponDto>>(stringResponse);
        Assert.IsNotNull(tmpList);
        Assert.AreEqual(2, tmpList.Count);
    }

    [TestInitialize]
    public async Task addCouponBeforeTest()
    {
        List<AddCouponRequest> couponsToAdd = new List<AddCouponRequest> {
            new AddCouponRequest("JUNE_10_PERCENT", 10, DateTime.Now, DateTime.Now.AddDays(10)),
            new AddCouponRequest("JULY_10_PERCENT", 5, DateTime.Now, DateTime.Now.AddDays(1)),
        };
        
        var token = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        for (int i = 0; i < couponsToAdd.Count; i++)
        {
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(couponsToAdd[i]), Encoding.UTF8, "application/json");
            try //Throws a C# exception for handeling an I/O, but still goes threw
            {
                _ = await client.PostAsync("api/coupon", jsonContent);
            }
            catch (Exception) { }
 
        }
    }
}
