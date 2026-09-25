using Persistence; // Yazdığımız extension metodun kütüphanesi

var builder = WebApplication.CreateBuilder(args);

// PERSISTENCE KATMANINI SİSTEME DAHİL EDİYORUZ
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Identity kullandığımız için Authentication'ı (Kimlik Doğrulama) aktif ediyoruz
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();