using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Modulos
{
    public class DeleteModuloEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapDelete("/{id}", HandleAsync)
              .WithName("Modulo: Delete")
              .WithSummary("Excluir um modulo")
              .WithDescription("Excluir um modulo")
              .WithOrder(3)
              .Produces<Response<Modulo?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IModuloHandler handler,
            long id)
        {
            var request = new DeleteModuloRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
