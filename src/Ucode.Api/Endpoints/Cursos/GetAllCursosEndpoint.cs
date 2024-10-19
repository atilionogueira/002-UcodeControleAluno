using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Cursos
{
    public class GetAllCursosEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Curso: Get All")
            .WithSummary("Recuperando todos os curso")
            .WithDescription("Recuperando todos curso")
            .WithOrder(5)
            .Produces<PagedResponse<List<Curso>?>>();
        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICursoHandler handler,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery]int pageSize = Configuration.DefaultPageSize )
        {
            var request = new GetAllCursoRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
