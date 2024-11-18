using Microsoft.EntityFrameworkCore;
using NotizAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Verbindungszeichenfolge aus appsettings.json abrufen
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// MySQL-Datenbank mit Pomelo.EntityFrameworkCore registrieren
builder.Services.AddDbContext<NotizenContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();

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

app.Run();
