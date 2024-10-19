using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;


namespace Ucode.Api.Endpoints.Cursos
{
    public class UpdateCursoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPut("/{id}", HandleAsync)
            .WithName("Curso: Update")
            .WithSummary("Atualizar um curso")
            .WithDescription("Atualizar um curso")
            .WithOrder(2)
            .Produces<Response<Curso?>>();
        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICursoHandler handler,
            UpdateCursoRequest request,
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
