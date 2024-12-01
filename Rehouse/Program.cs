using NLog;
using NLog.Web;
using Yungching.Common;

// ���T���J NLog �]�w
var nlogConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");

var logger = NLog.LogManager.GetCurrentClassLogger();
try
{
    logger.Debug("��l�����ε{��...");
    var builder = WebApplication.CreateBuilder(args);

    // �T�O�N NLog �]�w��X�� .NET Core ����x�t��
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

    logger.Info("���ε{���Ұʧ���");
    app.Run();
}
catch (Exception ex)
{
    // ����ҰʹL�{�������~
    logger.Error(ex, "���ε{���ҰʹL�{������");
    throw;
}
finally
{
    // �T�O���T���� NLog
    LogManager.Shutdown();
}