using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Modulos
{
    public class CreateModuloEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapPost("/", HandleAsync)
            .WithName("Modulos: Create")
            .WithSummary("Cria um novo modulo")
            .WithDescription("Cria um novo modulo")
            .WithOrder(1)
            .Produces<Response<Modulo?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IModuloHandler handler,
            CreateModuloRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            return result.IsSucess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
