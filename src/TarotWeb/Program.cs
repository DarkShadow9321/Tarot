using Microsoft.EntityFrameworkCore;
using TarotWeb.Data;
using TarotWeb.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TarotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<TarotDrawService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Demo deliberately binds to localhost. Never expose histories or admin routes publicly
// without adding authentication, authorization and a supported migration strategy.
app.Use(async (context, next) =>
{
    if (context.Connection.RemoteIpAddress is { } remote &&
        !System.Net.IPAddress.IsLoopback(remote))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("This learning demo is available on localhost only.");
        return;
    }
    await next();
});

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// EnsureCreated is intentionally only for the local learning demo. It does not apply migrations.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TarotDbContext>();
    try
    {
        await db.Database.EnsureCreatedAsync();
        await TarotSeed.SeedAsync(db);
    }
    catch (Exception ex)
    {
        app.Logger.LogCritical(ex, "SQL Server database initialization failed. Check README and connection string.");
        throw;
    }
}
await app.RunAsync();
