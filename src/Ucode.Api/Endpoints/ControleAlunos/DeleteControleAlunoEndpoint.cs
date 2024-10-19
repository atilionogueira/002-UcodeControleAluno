using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.ControleAlunos
{
    public class DeleteControleAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapDelete("/{id}", HandleAsync)
              .WithName("ControleAlunos: Delete")
              .WithSummary("Excluir um ontrole de aluno")
              .WithDescription("Excluir um controle de aluno")
              .WithOrder(3)
              .Produces<Response<ControleAluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IControleAlunoHandler handler,
            long id)
        {
            var request = new DeleteControleAlunoRequest
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
