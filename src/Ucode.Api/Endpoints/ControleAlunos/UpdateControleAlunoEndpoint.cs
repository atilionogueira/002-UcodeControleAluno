using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.ControleAlunos
{
    public class UpdateControleAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapPut("/{id}", HandleAsync)
              .WithName("ControldeAlunos: Update")
              .WithSummary("Atualizar um controle de aluno")
              .WithDescription("Atualizar um controle de aluno")
              .WithOrder(2)
              .Produces<Response<ControleAluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IControleAlunoHandler handler,
            UpdateControleAlunoRequest request,
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
