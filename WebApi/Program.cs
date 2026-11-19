using Application; // Yazdığımız extension metodun kütüphanesi
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Persistence; // Yazdığımız extension metodun kütüphanesi
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// PERSISTENCE, APPLICATION VE INFRASTRUCTURE KATMANLARINI SİSTEME DAHİL EDİYORUZ
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// --- JWT AUTHENTICATION (KİMLİK DOĞRULAMA) AYARLARI ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    // Identity varsayılan olarak Cookie şeması atar; API'nin JWT kullanması için bunu eziyoruz
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
        ClockSkew = TimeSpan.Zero // Token süresi dolduğu saniye geçersiz olsun (varsayılan 5 dk toleransı sıfırlar)
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- SWAGGER AUTHORIZE (KİLİT BUTONU) AYARLARI ---
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Login olduktan sonra aldığınız JWT token'ı buraya yapıştırın (Başına 'Bearer' yazmanıza gerek yok)."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Identity ve JWT için sıralama kritiktir: Önce kimlik doğrulama, sonra yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();