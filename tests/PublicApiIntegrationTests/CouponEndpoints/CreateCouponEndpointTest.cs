

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace PublicApiIntegrationTests.CouponEndpoints;
[TestClass]
public class CreateCouponEndpointTest
{

    [TestMethod]
    public async Task ReturnsNotAuthorizedGivenNormalUserToken()
    {
        var jsonContent = GetValidNewItemJson();
        var token = ApiTokenHelper.GetNormalUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsync("api/coupon", jsonContent);

        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task ReturnsSuccessGivenValidNewCouponAndAdminUserToken()
    {
        var jsonContent = GetValidNewItemJson();
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        var response = await client.PostAsync("api/coupon", jsonContent);
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<AddCouponResponse>();

        Assert.IsTrue(model.CreatedCoupon);
    }

    private StringContent GetValidNewItemJson()
    {
        var request = getCoupon();
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        return jsonContent;
    }

    private AddCouponRequest getCoupon()
    {
        return new AddCouponRequest()
        {
            Name = "name",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2),
            PercentageDiscount = 12
        };
    }
}
