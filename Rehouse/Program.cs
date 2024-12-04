using Biomedica.NGS.Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Repository;
using Yungching.Repository;
using Yungching.Repository.Interface;
using Yungching.Repository.Models;
using Yungching.Service;
using Yungching.Service.Interface;

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
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddScoped<ApiLoggingFilterAttribute>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    builder.Services.AddScoped<IEstateService,EstateService>();
    builder.Services.AddScoped<IEstateRepository,EstateRepository>();


    //1.取得組態中資料庫連線設定
    string? connectionString = builder.Configuration.GetConnectionString("RehouseContext");

    //2.註冊EF Core的DbContext
    builder.Services.AddDbContext<RehouseContext>(options => options.UseSqlServer(connectionString));


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
        pattern: "{controller=Estates}/{action=Index}/{id?}");

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