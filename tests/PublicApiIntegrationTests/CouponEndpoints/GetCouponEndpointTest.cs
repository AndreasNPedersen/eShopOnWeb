using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;
//using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Azure.Core;
using System.Text.Json;

namespace PublicApiIntegrationTests.CouponEndpoints;

public class GetCouponEndpointTest : IAsyncLifetime
{
    public GetCouponEndpointTest() {
        
    }

    [Fact(DisplayName = "Successful getCouponRequest")]
    public async void testGetCoupon()
    {
        var response = await ProgramTest.NewClient.GetAsync("api/coupons/1");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GetByIdCouponResponse>();

        Assert.IsNotNull(model);
        Assert.AreEqual(1, model.Coupon.Id);
        Assert.AreEqual("MAY_10_PERCENT", model.Coupon.Name);


    }

    private async Task addCouponBeforeTest()
    {
        AddCouponRequest request = new AddCouponRequest();
        request.Name = "MAY_10_PERCENT";
        request.StartDate = DateTime.Now;
        request.EndDate = DateTime.Now.AddDays(10);
        request.PercentageDiscount = 10;
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await ProgramTest.NewClient.PostAsync("api/coupons", jsonContent);
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.Run(() => Console.WriteLine("yay"));
    }

    Task IAsyncLifetime.InitializeAsync()
    {
        return Task.Run(addCouponBeforeTest);
    }
}
