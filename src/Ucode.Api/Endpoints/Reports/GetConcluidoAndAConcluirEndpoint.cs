using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models.Reports;
using Ucode.Core.Requests.Reports;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Reports
{
    public class GetConcluidoAndAConcluirEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/expenses", HandleAsync)
            .Produces<Response<List<ConcluidoAndAConcluir>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler)
        {
            var request = new GetConcluidoAndAConcluirRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetConcluidoAndAConcluirAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
