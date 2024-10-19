using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Alunos
{
    public class DeleteAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)                       
          => app.MapDelete("/{id}", HandleAsync)
              .WithName("Alunos: Delete")
              .WithSummary("Excluir um aluno")
              .WithDescription("Excluir um aluno")
              .WithOrder(3)
              .Produces<Response<Aluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IAlunoHandler handler,           
            long id)
        {
            var request = new DeleteAlunoRequest
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
