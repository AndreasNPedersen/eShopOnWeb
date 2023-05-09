using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Xunit.Abstractions;
using Xunit;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Ardalis.GuardClauses;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.CouponReposistoryTests;
public class DeleteCoupon
{
    private readonly CatalogContext _catalogContext;
    private readonly EfRepository<Coupon> _couponRepository;
    private readonly ITestOutputHelper _output;
    private readonly Coupon existingCoupon = new Coupon(0, "test", 12, DateTime.Now, DateTime.Now.AddDays(2));
    public DeleteCoupon(ITestOutputHelper output)
    {
        _output = output;
        var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestCatalog")
            .Options;
        _catalogContext = new CatalogContext(dbOptions);
        _couponRepository = new EfRepository<Coupon>(_catalogContext);
        Setup();
    }

    public void Setup()
    {
        _catalogContext.Coupons.Add(existingCoupon);
        _catalogContext.SaveChanges();
    }

    [Fact]
    public async Task DeleteCouponSuccess()
    {
        //arrange
        int couponId = existingCoupon.Id;

        //is the coupon in the system test
        var couponFromRepo = await _couponRepository.GetByIdAsync(couponId);
        Assert.Equal(existingCoupon.Id, couponFromRepo.Id);
        
        //test the deletion
        _=_couponRepository.DeleteAsync(couponFromRepo);
        var tryGetDeletedCoupon = await _couponRepository.GetByIdAsync(couponId);
        Assert.Null(tryGetDeletedCoupon);

    }
}
