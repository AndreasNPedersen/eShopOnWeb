﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints.AddCoupons;
//using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Azure.Core;
using System.Text.Json;
using Microsoft.eShopWeb.PublicApi.CouponEndpoints;
using System.Net;

namespace PublicApiIntegrationTests.CouponEndpoints;


[TestClass]
public class GetCouponEndpointTest 
{
    public int couponId = 2;
    
    [TestMethod]
    public async Task testGetCoupon()
    {
        var token = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/coupon/" + couponId);
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<CouponDto>();
        

        Assert.IsNotNull(model);
        Assert.AreEqual(2, model.Id);
        Assert.AreEqual("JUNE_10_PERCENT", model.Name);
    }


    [TestMethod]
    public async Task testFailureCouponNotFound()
    {
        int nonExistantCouponId = 101;
        var token = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/coupon/" + nonExistantCouponId);

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }



    [ClassInitialize]
    public static void addCouponBeforeTest(TestContext context)
    {
        AddCouponRequest request = new AddCouponRequest();
        request.Name = "JUNE_10_PERCENT";
        request.StartDate = DateTime.Now;
        request.EndDate = DateTime.Now.AddDays(10);
        request.PercentageDiscount = 10;

        var token = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        //Throws a C# exception for handeling an I/O, but still goes threw 
        try
        {
            var response = client.PostAsync("api/coupon", jsonContent).Result;
        }
        catch (Exception) { }
    }
}
