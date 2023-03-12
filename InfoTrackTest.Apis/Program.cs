using Core;
using InfoTrackTest.Repositories.Context;
using InfoTrackTest.Repositories.Repositories;
using InfoTrackTest.Services.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Auto add services and repositories
var connectionString = builder.Configuration.GetConnectionString("InfoTrackTestDb");
builder.Services.AddDbContext<InfoTrackTestContext>((options) =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
}, ServiceLifetime.Scoped);

builder.Services.Scan(scan => scan
    .FromCallingAssembly()
    .FromAssemblies(typeof(IInfoTrackTestService).Assembly, typeof(IInfoTrackTestRepository).Assembly)
    .AddClasses()
      .AsImplementedInterfaces()
      .WithScopedLifetime());
#endregion

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .WriteTo.File("logs/info-track-text-logs.txt", rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var origins = builder.Configuration.GetValue<string>("Cors:Origins")?.Split(',');
app.UseCors(
    options => options.WithOrigins(origins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .WithExposedHeaders("Content-Disposition"));

app.UseAuthorization();

app.Use(async (context, next) =>
{
    using var scope = app.Services.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<InfoTrackTestContext>();

    await db.Database.EnsureCreatedAsync();
    await db.Database.MigrateAsync();

    await next(context);
});

app.MapControllers();

app.Run();