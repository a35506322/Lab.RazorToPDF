# 專案套件及使用說明

## 專案概述

主要功能利用 Razor 視圖渲染，並使用 Puppeteer 生成 PDF

## 主要使用套件

### 1. PuppeteerSharp

PuppeteerSharp 是一個 .NET 的 Puppeteer 實現，用於控制無頭瀏覽器並生成 PDF。

```csharp
// 安裝指令
dotnet add package PuppeteerSharp
```

**關鍵功能：**

- 網頁渲染和 PDF 生成
- 無頭瀏覽器自動化

**使用範例：**

```csharp
// 初始化瀏覽器
var browser = await Puppeteer.LaunchAsync(
    new LaunchOptions {
        Headless = true,
        ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
    }
);

// 建立新頁面並設置內容
using var page = await browser.NewPageAsync();
await page.SetContentAsync(htmlContent);

// 生成 PDF
var pdfBytes = await page.PdfDataAsync(
    new PdfOptions {
        Format = PuppeteerSharp.Media.PaperFormat.A4,
        PrintBackground = true,
        Scale = 1
    }
);
```

### 2. Razor.Templating.Core

Razor.Templating.Core 是一個強大的套件，用於將 Razor 視圖轉換為 HTML 字串，無需完整的 MVC 架構。

```csharp
// 安裝指令
dotnet add package Razor.Templating.Core
```

**關鍵功能：**

- 在非 Web 應用或 API 專案中渲染 Razor 視圖
- 支援視圖模型綁定和部分視圖
- 輕鬆生成 HTML 電子郵件模板或報表

**使用範例：**

```csharp
// 註冊服務
builder.Services.AddRazorTemplating();

// 使用服務
app.MapGet("/get-example-view", async ([FromServices] IRazorTemplateEngine razorTemplateEngine) =>
{
    // 不帶模型渲染
    var template = await razorTemplateEngine.RenderAsync("/Views/Home/Index.cshtml");
    return Results.Ok(template);
});

// 使用模型渲染視圖
app.MapGet("/get-todo-list-html", async ([FromServices] IRazorTemplateEngine razorTemplateEngine) =>
{
    var viewModel = new TodoListViewModel { TodoItems = todoItems };
    var template = await razorTemplateEngine.RenderAsync("/Views/Todo/TodoListPdf.cshtml", viewModel);
    return Results.Content(template, "text/html");
});
```

## 開始使用

### 環境要求

- .NET 8.0 或更高版本
- Google Chrome 瀏覽器（用於 Puppeteer）
- 適當的 IDE（推薦 Visual Studio 2022 或 Visual Studio Code）

### 安裝步驟

1. 複製專案到本地：

   ```
   git clone [專案儲存庫 URL]
   ```

2. 安裝所需套件：

   ```
   dotnet restore
   ```

3. 確認 Chrome 瀏覽器路徑：
   請確保 `PuppeteerService.cs` 中的 Chrome 路徑與您系統上的實際安裝位置一致：

   ```csharp
   ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
   ```

4. 運行專案：
   ```
   dotnet run
   ```

### 配置 Razor.Templating.Core

此套件需要正確設置視圖搜索路徑：

1. 確保視圖文件位於正確的目錄結構中（通常是 `/Views` 目錄）

### 配置 PDF 生成

可以在程式碼中根據需要調整 PDF 設置：

```csharp
var pdfBytes = await page.PdfDataAsync(
    new PdfOptions
    {
        Format = PuppeteerSharp.Media.PaperFormat.A4,  // 設置紙張大小
        PrintBackground = true,                        // 包含背景
        Scale = 1,                                     // 縮放比例
        // 其他設置選項
    }
);
```

## 常見問題解決

1. **問題：無法找到 Razor 視圖**

   解決方案：確保視圖路徑正確，並且視圖遵循正確的命名約定。檢查項目引用和目錄結構。

2. **問題：Chrome 瀏覽器路徑不正確**

   解決方案：修改 `PuppeteerService.cs` 中的 `ExecutablePath` 為您系統上的 Chrome 實際位置。

3. **問題：PDF 生成格式不正確**

   解決方案：調整 `PdfOptions` 參數，例如設置邊距、紙張大小等。確保 CSS 樣式適合於打印。
