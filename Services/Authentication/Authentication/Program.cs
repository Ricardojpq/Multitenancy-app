using Authentication;
using Authentication.Persistence.Context;
using Destructurama;
using Duende.IdentityServer.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));
builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseDeveloperExceptionPage();
    IdentityModelEventSource.ShowPII = true;
}
else
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

// Needed for running over http (dev only)
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
});

app.UseCors(ServicesConfiguration.CorsRuleName);
app.UseStaticFiles();
app.UseForwardedHeaders();
app.UseIdentityServer();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .MinimumLevel.Debug()
    .Destructure.JsonNetTypes()
    .WriteTo.Console()
    .CreateLogger();
Log.Debug("Successfully Setup Serilog");

#region InicializacionDB

if (!app.Environment.IsDevelopment())
{
    IdentityServerDbInitializer.Initialize(app, builder.Configuration);
}

var seedTestUser = builder.Configuration.GetValue<bool>("SeedTestUser");
var tenantIdTestUser = builder.Configuration.GetValue<string>("TenantId");
AuthenticationApiDbInitializer.InitializeAsync(app, seedTestUser).GetAwaiter().GetResult();
#endregion

app.Run();
