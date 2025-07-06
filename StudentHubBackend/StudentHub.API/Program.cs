using Microsoft.EntityFrameworkCore;
using StudentHub.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StudentHub.Application.Students.Interfaces;
using StudentHub.Infrastructure.Persistence;
using StudentHub.Infrastructure.Repositories;
using StudentHub.Infrastructure.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Application.CreditPrograms.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

//Inyección de Interfaces y Repositorios
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ICreditProgramRepository, CreditProgramRepository>();


// JWT Config
var key = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.UTF8.GetBytes(key!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

builder.Services.AddAuthorization();


// MediatR v13:
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
