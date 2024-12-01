using NLog;
using NLog.Web;
using Yungching.Common;

// 明確載入 NLog 設定
var nlogConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");

var logger = NLog.LogManager.GetCurrentClassLogger();
try
{
    logger.Debug("初始化應用程式...");
    var builder = WebApplication.CreateBuilder(args);

    // 確保將 NLog 設定整合到 .NET Core 的日誌系統
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    logger.Info("應用程式啟動完成");
    app.Run();
}
catch (Exception ex)
{
    // 捕獲啟動過程中的錯誤
    logger.Error(ex, "應用程式啟動過程中停止");
    throw;
}
finally
{
    // 確保正確關閉 NLog
    LogManager.Shutdown();
}