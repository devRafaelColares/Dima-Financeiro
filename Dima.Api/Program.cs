using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var CnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => 
    {
        x.UseSqlServer(CnnStr);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet
        ("/v1/categories", async
        (
        ICategoryHandler handler) => 
        {
            var request = new GetAllCategoriesRequest { UserId = "teste@balta.io" };
            return await handler.GetAllCategoriesAsync(request);
        })
        .WithName("GetAllCategories")
        .WithSummary("Get all categories")
        .Produces<PagedResponse<List<Category>?>>();

app.MapGet
        ("/v1/categories{id}", async
        (long id,
        ICategoryHandler handler) => 
        {
            var request = new GetCategoryByIdRequest { Id = id, UserId = "teste@balta.io" };
            return await handler.GetCategoryByIdAsync(request);
        })
        .WithName("GetByIdCategories")
        .WithSummary("Get a category by id")
        .Produces<Response<Category?>>();

app.MapPost
        ("/v1/categories", async
        (CreateCategoryRequest request, ICategoryHandler handler) => await
        handler.CreateCategoryAsync(request))
        .WithName("CreateCategories")
        .WithSummary("Create a new category")
        .Produces<Response<Category?>>();

app.MapPut
        ("/v1/categories{id}", async
        (long id,
        UpdateCategoryRequest request, ICategoryHandler handler) => 
        {
            request.Id = id;
            return await handler.UpdateCategoryAsync(request);
        })
        .WithName("UpdateCategories")
        .WithSummary("Update a category")
        .Produces<Response<Category?>>();

app.MapDelete
        ("/v1/categories{id}", async
        (long id,
        ICategoryHandler handler) => 
        {
            var request = new DeleteCategoryRequest { Id = id, UserId = "teste@balta.io" };
            return await handler.DeleteCategoryAsync(request);
        })
        .WithName("DeleteCategories")
        .WithSummary("Delete a category")
        .Produces<Response<Category?>>();



app.Run();

