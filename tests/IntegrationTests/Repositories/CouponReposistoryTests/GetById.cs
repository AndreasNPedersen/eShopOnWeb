using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit.Abstractions;
using Xunit;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.CouponReposistoryTests;

/// <summary>
/// Test Database for getting a coupon Id
/// </summary>
public class GetById
{
    private readonly CatalogContext _catalogContext;
    private readonly EfRepository<Coupon> _couponRepository;
    private readonly ITestOutputHelper _output;
    public GetById(ITestOutputHelper output)
    {
        _output = output;
        var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestGetCoupon")
            .Options;
        _catalogContext = new CatalogContext(dbOptions);
        _couponRepository = new EfRepository<Coupon>(_catalogContext);
    }

    [Fact]
    public async Task GetsExistingCoupon()
    {
        var existingCoupon = new Coupon(0,"test",12,DateTime.Now,DateTime.Now.AddDays(2));
        _catalogContext.Coupons.Add(existingCoupon);
        _catalogContext.SaveChanges();
        int couponId = existingCoupon.Id;
        _output.WriteLine($"OrderId: {couponId}");

        var orderFromRepo = await _couponRepository.GetByIdAsync(couponId);
        Assert.Equal(existingCoupon.Id, orderFromRepo.Id);

    }
}
