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


namespace Microsoft.eShopWeb.IntegrationTests.Repositories.CouponReposistoryTests;
public class AddCoupon
{
    private readonly CatalogContext _catalogContext;
    private readonly EfRepository<Coupon> _couponRepository;
    private readonly ITestOutputHelper _output;
    private Coupon _coupon = new ();
    public AddCoupon(ITestOutputHelper output)
    {
        _output = output;
        var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestAddCoupon")
            .Options;
        _catalogContext = new CatalogContext(dbOptions);
        _couponRepository = new EfRepository<Coupon>(_catalogContext);
    }


    [Fact]
    public async Task GetAndDeleteExistingCoupon()
    {
        int id = 1;
        string couponCode = "test1";
        int percentageDiscount = 20;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(2);
        _coupon = new Coupon(id, couponCode, percentageDiscount, startDate, endDate);

        await _couponRepository.AddAsync(_coupon);
        await _couponRepository.SaveChangesAsync();


        var checkAddedCoupon = await _couponRepository.ListAsync();
        Assert.Equivalent(_coupon, checkAddedCoupon[0]);
        

    }
}
