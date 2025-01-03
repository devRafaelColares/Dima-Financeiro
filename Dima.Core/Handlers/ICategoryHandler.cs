using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateCategoryAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> GetCategoryByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>>> GetAllCategoriesAsync(GetAllCategoriesRequest request);
    Task<Response<Category?>> DeleteCategoryAsync(DeleteCategoryRequest request);
}