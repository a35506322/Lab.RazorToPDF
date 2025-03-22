using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;
using ReazorTemplate.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddControllersWithViews();

builder.Services.AddRazorTemplating();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseStaticFiles();

app.MapGet(
    "/get-example-view",
    async ([FromServices] IRazorTemplateEngine razorTemplateEngine) =>
    {
        var template = await razorTemplateEngine.RenderAsync("/Views/Home/Index.cshtml");
        return Results.Ok(template);
    }
);

app.MapGet(
    "/get-todo-list-html",
    async ([FromServices] IRazorTemplateEngine razorTemplateEngine) =>
    {
        List<TodoItem> _todoItems = new List<TodoItem>
        {
            new TodoItem
            {
                Id = 1,
                Title = "完成專案文件",
                Content = "撰寫專案需求文件和系統設計文件",
                IsComplete = false,
                CompleteTime = null,
            },
            new TodoItem
            {
                Id = 2,
                Title = "開發登入功能",
                Content = "實現用戶登入和驗證功能",
                IsComplete = true,
                CompleteTime = DateTime.Now.AddDays(-2),
            },
            new TodoItem
            {
                Id = 3,
                Title = "規劃資料庫結構",
                Content = "設計資料庫表格和關聯",
                IsComplete = false,
                CompleteTime = null,
            },
        };

        var viewModel = new TodoListViewModel { TodoItems = _todoItems };
        var template = await razorTemplateEngine.RenderAsync(
            "/Views/Todo/TodoListPdf.cshtml",
            viewModel
        );
        return Results.Ok(template);
    }
);

app.MapGet(
    "/preview-any-view/{*path}",
    async ([FromRoute] string path, [FromServices] IRazorTemplateEngine razorTemplateEngine) =>
    {
        try
        {
            // 確保路徑格式正確
            if (!path.StartsWith("/"))
                path = $"/{path}";

            if (!path.EndsWith(".cshtml"))
                path = $"{path}.cshtml";

            // 根據視圖路徑判斷適當的模型
            object? model = path.Contains("/Todo/")
                ? new TodoListViewModel { TodoItems = GetSampleTodoItems() }
                : null;

            var html = await razorTemplateEngine.RenderAsync(path, model);
            return Results.Content(html, "text/html");
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"視圖渲染錯誤: {ex.Message}");
        }
    }
);

List<TodoItem> GetSampleTodoItems()
{
    return new List<TodoItem>
    {
        new TodoItem
        {
            Id = 1,
            Title = "完成專案文件",
            Content = "撰寫專案需求文件和系統設計文件",
            IsComplete = false,
            CompleteTime = null,
        },
        new TodoItem
        {
            Id = 2,
            Title = "開發登入功能",
            Content = "實現用戶登入和驗證功能",
            IsComplete = true,
            CompleteTime = DateTime.Now.AddDays(-2),
        },
        // 可以添加更多示範資料
    };
}

app.Run();
