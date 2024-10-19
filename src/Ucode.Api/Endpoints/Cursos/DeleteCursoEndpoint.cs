using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Cursos
{
    public class DeleteCursoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapDelete("/{id}", HandleAsync)
            .WithName("Curso: Delete")
            .WithSummary("Excluindo um curso")
            .WithDescription("Excluindo um curso")
            .WithOrder(3)
            .Produces<Response<Curso?>>();
        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICursoHandler handler,          
            long id)
        {
            var request = new DeleteCursoRequest
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
