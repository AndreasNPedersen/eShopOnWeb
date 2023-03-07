using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;
using MinimalApi.Endpoint;
using IResult = Ardalis.Result.IResult;

namespace Microsoft.eShopWeb.PublicApi.CouponEndPoint;

public class CouponGetEndpoint : IEndpoint<IResult, IRepository<Coupon>>
{
    
    private readonly IMapper _mapper;

    public CouponGetEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }



    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Coupon",
                async (IRepository<Coupon> catalogTypeRepository) =>
                {
                    return await HandleAsync(catalogTypeRepository);
                })
            .Produces<ListCatalogTypesResponse>()
            .WithTags("CouponGetEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<Coupon> catalogTypeRepository)
    {
        var response = new ListCatalogTypesResponse();

        var items = await catalogTypeRepository.ListAsync();

        response.CatalogTypes.AddRange(items.Select(_mapper.Map<CouponDto>));

        return Results.Ok(response);
    }   
}
