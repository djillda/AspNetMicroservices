using Catalog.API.Data;
using Catalog.API.Data.Interfaces;
using Catalog.API.Repositories;
using Catalog.API.Repositories.Interfaces;
using HealthChecks.MongoDb;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using ServiceStack.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); 

//builder.Services.AddRazorPages();

//builder.Services.AddMvcCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
});

//builder.Services.AddSingleton(new MongoDbHealthCheck(Config.DatabaseSettings.DatabaseConnectionString, config.DatabaseSettings.DatabaseName));
//builder.Services.AddHealthChecks().AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1");
        //c.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/api/v1/Catalog/GetProducts", async context =>
//    {
//        await context.Response.WriteAsync("Hello, World!");
//    });
//});

//app.UseAuthorization();

//app.MapRazorPages();

app.Run();
