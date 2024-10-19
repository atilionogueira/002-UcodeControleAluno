using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.ControleAlunos
{
    public class CreateControleAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/", HandleAsync)
            .WithName("ControleAlunos: Create")
            .WithSummary("Cria um novo Controle de Aluno")
            .WithDescription("Cria um novo Controlde de Aluno")
            .WithOrder(1)
            .Produces<Response<ControleAluno?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IControleAlunoHandler handler,
            CreateControleAlunoRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            return result.IsSucess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
