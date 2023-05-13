using MyCookBook.Api.Filters;
using MyCookBook.Application.Services.Automapper;
using MyCookBook.Application;
using MyCookBook.Domain.Extension;
using MyCookBook.Infrastructure;
using MyCookBook.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg => 
{
    cfg.AddProfile(new AutoMapperConfig());
}).CreateMapper());

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

app.Run();

void UpdateDataBase() 
{
    var connection = builder.Configuration.GetConnection();
    var databaseName = builder.Configuration.GetDatabaseName();

    Database.CreateDatabase(connection, databaseName);

    app.MigrateDatabase();
}
