using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;


namespace Dima.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request)
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

                return new Response<Category?>(category, 201, "Categoria criada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Response<Category?>(null, 500, "Erro ao criar categoria");
            }
        }

        public async Task<Response<Category?>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                    category.Title = request.Title;
                    category.Description = request.Description;

                    context.Categories.Update(category);
                    await context.SaveChangesAsync();

                    return new Response<Category?>(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Response<Category?>(null, 500, "[FP079] Erro ao atualizar categoria");
            }
        }

        public async Task<Response<Category?>> GetCategoryByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return category is null 
                    ? new Response<Category?>(null, 404, "Categoria não encontrada") 
                    : new Response<Category?>(category);
            }
            catch
            {
                
                return new Response<Category?>(null, 500, "Erro ao excluir categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllCategoriesAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context.Categories
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Title);

                var categories = await query
                    .Skip((request.PageNumber -1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(
                    categories,
                    count, request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>>(null, 500, "Erro ao buscar categorias");
            }
        }

        public async Task<Response<Category?>> DeleteCategoryAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();

                    return new Response<Category?>(category, 200, "Categoria excluída com sucesso");
            }
            catch 
            {
                return new Response<Category?>(null, 500, "Erro ao excluir categoria");
            }
        }
    }
}