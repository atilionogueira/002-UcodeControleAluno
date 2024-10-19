using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Modulos
{
    public class UpdateModuloEndpoit : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPut("/{id}", HandleAsync)
              .WithName("Modulo: Update")
              .WithSummary("Atualizar um modulo")
              .WithDescription("Atualizar um modulo")
              .WithOrder(2)
              .Produces<Response<Modulo?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IModuloHandler handler,
            UpdateModuloRequest request,
            long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
