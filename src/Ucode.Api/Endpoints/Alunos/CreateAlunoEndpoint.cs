using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;


namespace Ucode.Api.Endpoints.Alunos
{
    public class CreateAlunoEndpoint : IEndpoint
    {    
        public static void Map(IEndpointRouteBuilder app) 
          => app.MapPost("/", HandleAsync)
            .WithName("Alunos: Create")
            .WithSummary("Cria um novo aluno")
            .WithDescription("Cria um novo aluno")
            .WithOrder(1)
            .Produces<Response<Aluno?>>();

            private static async Task<IResult> HandleAsync(
                ClaimsPrincipal user,
                IAlunoHandler handler,
                CreateAlunoRequest request)
            {
                request.UserId = user.Identity?.Name ?? string.Empty;
                var result = await handler.CreateAsync(request);

                return result.IsSucess
                    ? TypedResults.Created($"/{result.Data?.Id}", result)
                    : TypedResults.BadRequest(result);
            }
    }
}
