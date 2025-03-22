using API;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using Razor.Templating.Core;
using ReazorTemplate.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorTemplating();

builder.Services.AddSingleton<PuppeteerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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
    async (
        [FromServices] IRazorTemplateEngine razorTemplateEngine,
        [FromServices] PuppeteerService puppeteerService
    ) =>
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

        var browser = await puppeteerService.GetBrowserAsync();
        using var page = await browser.NewPageAsync();
        await page.SetContentAsync(template);
        var pdfBytes = await page.PdfDataAsync();
        return Results.File(pdfBytes, "application/pdf", "output.pdf");
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

app.MapGet(
    "/get-loan",
    async (
        [FromServices] IRazorTemplateEngine razorTemplateEngine,
        [FromServices] PuppeteerService puppeteerService
    ) =>
    {
        // 建立假的 LoanViewModel 資料
        var loanModel = CreateSampleLoanViewModel();

        // 渲染 Razor 視圖並帶入模型
        var template = await razorTemplateEngine.RenderAsync("/Views/Loan/Loan.cshtml", loanModel);
        var browser = await puppeteerService.GetBrowserAsync();
        using var page = await browser.NewPageAsync();
        await page.SetContentAsync(template);
        var pdfBytes = await page.PdfDataAsync(
            new PdfOptions
            {
                Format = PuppeteerSharp.Media.PaperFormat.A4,
                PrintBackground = true,
                Scale = 1, // 縮小比例以適應內容
            }
        );
        return Results.File(pdfBytes, "application/pdf", "output.pdf");
    }
);

// 建立示範用的 LoanViewModel 資料
LoanViewModel CreateSampleLoanViewModel()
{
    return new LoanViewModel
    {
        // 訂單編號
        OrderNumber = "LN2023102500001",

        // 申請人基本資料
        ChineseName = "王大明",
        IdentityNumber = "A123456789",
        BirthYear = "75",
        BirthMonth = "05",
        BirthDay = "15",
        IdentityIssueYear = "110",
        IdentityIssueMonth = "03",
        IdentityIssueDay = "20",
        IdentityIssueType = 2, // 2.補發
        IssueLocation = "台北市",
        BirthCountry = "台灣",
        BirthCity = "台北",

        // 戶籍地址
        RegisteredPostalCode = "106",
        RegisteredCounty = "台北市",
        RegisteredTownship = "大安區",
        RegisteredVillage = "仁愛里",
        RegisteredRoad = "仁愛路四段",
        RegisteredLane = "151",
        RegisteredAlley = "12",
        RegisteredNumber = "5",
        RegisteredFloor = "7",

        // 居住地址
        IsSameAsRegistered = true,

        // 帳單地址
        BillingIsSameAsRegistered = true,
        BillingIsSameAsResidential = false,

        // 聯絡資訊
        ContactPhone = "02-27556789",
        MobilePhone = "0912-345-678",
        Email = "wangdaming@example.com",

        // 職業資料
        CompanyName = "聯合科技股份有限公司",
        JobTitle = "資深工程師",
        YearsOfService = "5",
        MonthlyIncome = 85000,
        Occupation = "資訊科技業",
        JobPosition = "技術開發",
        CompanyTaxId = "12345678",
        CompanyPhone = "02-87654321",
        CompanyAddress = "台北市內湖區瑞光路513巷32號7樓",

        // 所得資金來源
        IncomeSourceSalary = true,
        IncomeSourceInvestment = true,

        // 貸款資料
        IsOneTimeLoan = true,
        LoanAmount = 50,
        LoanTerm = 3,

        // 匯款資訊
        BankName = "聯邦銀行",
        BankAccountNumber = "123-456-789012",

        // 代償資料
        CompensationBankName_1 = "國泰世華銀行",
        CompensationBranchName_1 = "台北分行",
        CompensationAccountNumber_1 = "987-654-321098",
        CompensationAccountName_1 = "王大明",
        CompensationAmount_1 = 20,
        CompensationActualAmount_1 = 20,

        CompensationBankName_2 = "台新銀行",
        CompensationBranchName_2 = "信義分行",
        CompensationAccountNumber_2 = "456-789-123456",
        CompensationAccountName_2 = "王大明",
        CompensationAmount_2 = 15,
        CompensationActualAmount_2 = 15,

        // 申請資訊
        ApplyDate = "112年10月25日",

        // 銀行內部使用
        DepartmentName = "台北分行",
        PromotionStaff = "張經理",
        DepartmentCode = "TPE001",
    };
}

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
