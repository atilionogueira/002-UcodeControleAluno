using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Alunos
{
    public class GetAlunoByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapGet("/{id}", HandleAsync)
              .WithName("Alunos: Get By Id ")
              .WithSummary("Recupera um aluno")
              .WithDescription("Recupera um aluno")
              .WithOrder(4)
              .Produces<Response<Aluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IAlunoHandler handler,
            long id)
        {
            var request = new GetAlunoByIdRequest
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
