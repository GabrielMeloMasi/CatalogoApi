using CatalogoApi;
using CatalogoApi.DTOs.Mappings;
using CatalogoApi.Extensions;
using CatalogoApi.Filters;
using CatalogoApi.Logging;
using CatalogoApi.Repositories;
using CatalogoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using AutoMapper;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
})
    .AddJsonOptions(options => 
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection,
        ServerVersion.AutoDetect(connection)));

builder.Services.AddTransient<IMeuServico, MeuServico>();
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.DisableImplicitFromServicesParameters = true;
//});

builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Logging.AddProvider(new CustomerLoggerProvider(new CustomLoggerProviderConfiguration
//{
//    LogLevel = LogLevel.Information
//}));
builder.Services.AddAutoMapper(typeof(ProdutoDTOMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigueExceptionHandler(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
