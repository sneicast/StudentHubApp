using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentHub.API.Middleware;
using StudentHub.Application;
using StudentHub.Application.Classes.Interfaces;
using StudentHub.Application.CreditPrograms.Interfaces;
using StudentHub.Application.Students.Interfaces;
using StudentHub.Infrastructure.Persistence;
using StudentHub.Infrastructure.Repositories;
using StudentHub.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agrega la pol�tica CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

//Inyecci�n de Interfaces y Repositorios
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ICreditProgramRepository, CreditProgramRepository>();
builder.Services.AddScoped<IClassesRepository, ClassesRepository>();
builder.Services.AddScoped<IEnrollmentsRepository, EnrollmentsRepository>();
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

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
