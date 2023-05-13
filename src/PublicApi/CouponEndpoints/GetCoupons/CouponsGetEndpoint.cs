using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.GetCoupons;

public class CouponsGetEndpoint : IEndpoint<IResult, GetCouponListRequest, IRepository<Coupon>>
{
    private readonly IMapper _mapper;

    public CouponsGetEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Coupon",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async (int couponId, IRepository<Coupon> couponRepository) =>
            {
                return await HandleAsync(new GetCouponListRequest(), couponRepository);
            })
            .Produces<GetCouponListResponse>()
            .WithTags("CouponsGetEndpoint");
    }


    public async Task<IResult> HandleAsync(GetCouponListRequest request, IRepository<Coupon> couponRepository)
    {
        var response = new GetCouponListResponse();

        var coupons = await couponRepository.ListAsync();

        if (coupons is null)
        {
            return Results.Empty;
        }

        response.Coupons = coupons; 


        return Results.Ok(coupons);
    }
}
