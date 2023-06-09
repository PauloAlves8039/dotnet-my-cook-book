using MyCookBook.Api.Filters;
using MyCookBook.Application.Services.Automapper;
using MyCookBook.Application;
using MyCookBook.Domain.Extension;
using MyCookBook.Infrastructure;
using MyCookBook.Infrastructure.Migrations;
using MyCookBook.Infrastructure.RepositoryAccess;
using MyCookBook.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg => 
{
    cfg.AddProfile(new AutoMapperConfig());
}).CreateMapper());

builder.Services.AddScoped<AuthenticatedUserAttribute>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

UpdateDataBase();

app.UseMiddleware<CultureMiddleware>();

app.Run();

void UpdateDataBase() 
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    using var context = serviceScope.ServiceProvider.GetService<MyCookBookContext>();

    bool? dataBaseInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!dataBaseInMemory.HasValue || !dataBaseInMemory.Value) 
    {
        var connection = builder.Configuration.GetConnection();
        var databaseName = builder.Configuration.GetDatabaseName();

        Database.CreateDatabase(connection, databaseName);

        app.MigrateDatabase();
    }
}

#pragma warning disable CA1050, S3903, S1118
public partial class Program { }
#pragma warning restore CA1050, S3903, S1118
