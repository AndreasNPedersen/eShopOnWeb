
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CouponEndpoints.DeleteCoupons;

public class DeleteCouponEndpoint : IEndpoint<IResult, DeleteCouponRequest, IRepository<Coupon>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/DeleteCoupon/{couponId}",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (int couponId, IRepository<Coupon> itemRepository) =>
            {
                return await HandleAsync(new DeleteCouponRequest(couponId), itemRepository);
            })
            .Produces<DeleteCouponResponse>()
            .WithTags("CouponEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteCouponRequest request, IRepository<Coupon> itemRepository)
    {
        var response = new DeleteCouponResponse(request.CorrelationId());

        var itemToDelete = await itemRepository.GetByIdAsync(request.CouponId);
        if (itemToDelete is null)
        {
            response.DeletedStatus = false;
            return Results.NotFound(response);
        }

        await itemRepository.DeleteAsync(itemToDelete);

        return Results.Ok(response);
    }
}
