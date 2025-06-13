using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using yukumyolda.Persistence;
using yukumyolda.Domain.Entities;
using Scalar.AspNetCore;
using yukumyolda.Application.Features.Handlers.ProvinceHandlers;
using System.Reflection;
using yukumyolda.Application.Features.Queries.ProvinceQueries;
using FluentValidation;
using MediatR;
using System.Globalization;
using yukumyolda.Application.Beheviors;
using yukumyolda.Application;
using yukumyolda.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg =>
   cfg.RegisterServicesFromAssembly(typeof(GetProvinceQuery).Assembly));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();



builder.Services.AddDbContext<YukumYoldaContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("DefaultConnection connection string bulunamadı!");
    }
    options.UseSqlServer(connectionString);
});

// Identity konfigürasyonu
builder.Services.AddIdentityCore<User>(options =>
{ 
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;

    // Kullanıcı gereksinimleri
    options.User.RequireUniqueEmail = false; // Email zorunlu değil

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    // Oturum açma gereksinimleri
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon numarası onayı zorunlu
}).AddRoles<Role>()
.AddEntityFrameworkStores<YukumYoldaContext>()
.AddErrorDescriber<TurkishIdentityErrorDescriber>()
.AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "YukumYolda API Services";
        options.Theme = ScalarTheme.Moon;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.CustomCss = "";
        options.ShowSidebar = true;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
