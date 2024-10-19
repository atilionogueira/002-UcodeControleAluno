using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Modulos
{
    public class GetModuloByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/{id}", HandleAsync)
              .WithName("Modulos: Get By Id ")
              .WithSummary("Recupera um aluno")
              .WithDescription("Recupera um aluno")
              .WithOrder(4)
              .Produces<Response<Modulo?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IModuloHandler handler,
            long id)
        {
            var request = new GetModuloByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
