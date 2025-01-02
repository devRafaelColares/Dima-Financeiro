using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Api.Data;


namespace Dima.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category>(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Erro ao criar categoria");
            }
        }

        public async Task<Response<Category>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            // Implement your logic here
            return new Response<Category>();
        }

        public async Task<Response<Category>> GetCategoryByIdAsync(GetCategoryByIdRequest request)
        {
            // Implement your logic here
            return new Response<Category>();
        }

        public async Task<Response<List<Category>>> GetAllCategoriesAsync(GetAllCategoriesRequest request)
        {
            // Implement your logic here
            return new Response<List<Category>>();
        }

        public async Task<Response<Category>> DeleteCategoryAsync(DeleteCategoryRequest request)
        {
            // Implement your logic here
            return new Response<Category>();
        }
    }
}