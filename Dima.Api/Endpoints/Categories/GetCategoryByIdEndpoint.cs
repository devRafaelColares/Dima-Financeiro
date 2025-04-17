using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("GetCategoryById")
                .WithSummary("Get a category by id")
                .WithDescription("Get a category by id")
                .WithOrder(4)
                .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            long id)
        {
            var request = new GetCategoryByIdRequest
            { 
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetCategoryByIdAsync(request);

            return result.IsSuccess 
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);
        }
    }
}