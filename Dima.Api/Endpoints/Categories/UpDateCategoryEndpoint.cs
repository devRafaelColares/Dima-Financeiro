using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class UpDateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("UpDateCategory")
            .WithSummary("UpDate a category")
            .WithDescription("UpDate a category")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            UpdateCategoryRequest request,
            long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateCategoryAsync(request);
            
            return result.IsSuccess 
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);
        }
    }
}