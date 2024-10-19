using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Cursos
{
    public class CreateCursoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("", HandleAsync)
            .WithName("Curso: Create")
            .WithSummary("Criar um curso")
            .WithDescription("Criar um curso")
            .WithOrder(1)
            .Produces<Response<Curso?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICursoHandler handler,
            CreateCursoRequest request) 
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);
            return result.IsSucess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest();
        }
        
    }
}
