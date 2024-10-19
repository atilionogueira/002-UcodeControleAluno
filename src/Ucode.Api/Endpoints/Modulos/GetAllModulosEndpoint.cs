using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Responses;
using Ucode.Core;
using Ucode.Core.Requests.Modulo;
using System.Security.Claims;

namespace Ucode.Api.Endpoints.Modulos
{
    public class GetAllModulosEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/", HandleAsync)
              .WithName("Modulos: Get Al ")
              .WithSummary("Recupera todo os modulos")
              .WithDescription("Recupera todo os modulos")
              .WithOrder(5)
              .Produces<PagedResponse<List<Modulo>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IModuloHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSizer = Configuration.DefaultPageSize)

        {
            var request = new GetAllModuloRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSizer
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
