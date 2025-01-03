using Azure;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
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

app.MapGet("/", () => "Hello World!");

app.MapPost
        ("/v1/categories",
        (CreateCategoryRequest request, ICategoryHandler handler) => handler.CreateCategoryAsync(request))
        .WithName("CreateCategories")
        .WithSummary("Create a new category")
        .Produces<Response<Category>>();



app.Run();

