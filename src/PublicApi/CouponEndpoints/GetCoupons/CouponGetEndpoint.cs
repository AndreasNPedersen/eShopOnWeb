using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using AutoMapper;
using MinimalApi.Endpoint;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using static BlazorShared.Authorization.Constants;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;

public class CouponGetEndpoint : IEndpoint<IResult, GetByIdCouponRequest, IRepository<Coupon>>
{
    private readonly IMapper _mapper;

    public CouponGetEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Coupon",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (int couponId, IRepository<Coupon> couponRepository) =>
                {
                    return await HandleAsync(new GetByIdCouponRequest(couponId), couponRepository);
                })
            .Produces<CouponGetResponse>()
            .WithTags("CouponGetEndpoints");
    }


    public async Task<IResult> HandleAsync(GetByIdCouponRequest request, IRepository<Coupon> couponRepository)
    {
        var response = new GetByIdCouponResponse(request.CorrelationId());

        var coupon = await couponRepository.GetByIdAsync(request.CouponId);

        if (coupon is null)
        {
            return Results.NotFound();
        }

        response.Coupon = new CouponDto
        {
            Id = coupon.Id,
            Name = coupon.Name,
            PercentageDiscount = coupon.PercentageDiscount,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate

        };




        return Results.Ok(coupon);
    }

}
