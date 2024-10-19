using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Alunos
{
    public class UpdateAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapPut("/{id}", HandleAsync)
              .WithName("Alunos: Update")
              .WithSummary("Atualizar um aluno")
              .WithDescription("Atualizar um aluno")
              .WithOrder(2)
              .Produces<Response<Aluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IAlunoHandler handler,
            UpdateAlunoRequest request,
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
