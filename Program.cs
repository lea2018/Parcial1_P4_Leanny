using Parcial1_P4_Leanny.Services;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Registrar NumbersService
builder.Services.AddScoped<NumbersService>();

var app = builder.Build();

// Crear la tabla de números al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var numbersService = scope.ServiceProvider.GetRequiredService<NumbersService>();
    await numbersService.InitializeAsync();
}

// Configure the HTTP request pipeline.

    app.MapOpenApi();
    app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();