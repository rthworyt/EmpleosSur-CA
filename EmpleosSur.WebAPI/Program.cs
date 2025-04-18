using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Interfaces;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using EmpleosSur.WebAPI.Generators;
using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configuración de Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la base de datos
builder.Services.AddDbContext<EmpleosSurDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure())
);

// Inyección de dependencias
builder.Services.AddScoped<FakeDataGenerator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<ICandidatoService, CandidatoService>();

builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();

var app = builder.Build();

// Configuración de Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
