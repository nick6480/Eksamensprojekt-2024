using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC;
using MVC.DataAccess;
using MVC.Modules;
using MVC.Modules.DatabaseAdgang;
using MVC.Modules.APIService;

var builder = WebApplication.CreateBuilder(args);

// Configuration is now directly available
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();  // Adjust as necessary for your specific implementations
builder.Services.AddScoped<SQLQueryExecutor>(); // Assuming SQLQueryExecutor is also managed via DI
builder.Services.AddScoped<StoredFunctionExecutor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
