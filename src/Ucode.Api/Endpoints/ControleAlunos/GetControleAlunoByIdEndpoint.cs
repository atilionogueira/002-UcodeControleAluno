using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.ControleAlunos
{
    public class GetControleAlunoByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
              => app.MapGet("/{id}", HandleAsync)
              .WithName("Controle de Aluno: Get By Id ")
              .WithSummary("Recupera controle de aluno")
              .WithDescription("Recupera controlde de aluno")
              .WithOrder(4)
              .Produces<Response<ControleAluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IControleAlunoHandler handler,
            long id)
        {
            var request = new GetControleAlunoByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
