using PuppeteerSharp;

namespace API;

public class PuppeteerService : IDisposable
{
    private IBrowser? _browser;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private bool _disposed = false;

    public PuppeteerService()
    {
        // 建構式中不執行非同步初始化
    }

    public async Task<IBrowser> GetBrowserAsync()
    {
        if (_browser == null)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_browser == null) // 再次檢查，避免競爭條件
                {
                    _browser = await Puppeteer.LaunchAsync(
                        new LaunchOptions
                        {
                            Headless = true,
                            ExecutablePath =
                                @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                        }
                    );
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
        return _browser;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _browser?.Dispose();
            }

            _disposed = true;
        }
    }
}
