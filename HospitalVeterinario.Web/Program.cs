using HospitalVeterinario.Data;
using HospitalVeterinario.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts; // Para ApplicationPartManager
using Microsoft.AspNetCore.Mvc.Controllers;    // Para ControllerFeature
using Microsoft.AspNetCore.Routing;             // Para EndpointDataSource
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;             // Para LogLevel
using System.Collections.Generic;               // Para IEnumerable
using System.Linq;                              // Para SelectMany

var builder = WebApplication.CreateBuilder(args);



// --- INICIO DE CONFIGURACIÓN DE LOGGING MUY DETALLADO ---
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Routing", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Mvc", LogLevel.Debug);
// --- FIN DE CONFIGURACIÓN DE LOGGING MUY DETALLADO ---

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(Program).Assembly); // Especificamos explícitamente el ensamblado

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<HospitalVeterinario.Services.INominaService,
                           HospitalVeterinario.Services.NominaService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Volvemos a agregar los servicios para Swagger y API Explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar tus Repositorios
builder.Services.AddScoped<HospitalVeterinario.Data.Repositories.IEmpleadoRepository, HospitalVeterinario.Data.Repositories.EmpleadoRepository>();
builder.Services.AddScoped<HospitalVeterinario.Data.Repositories.IDepartamentoRepository, HospitalVeterinario.Data.Repositories.DepartamentoRepository>();
builder.Services.AddScoped<HospitalVeterinario.Data.Repositories.IPuestoRepository, HospitalVeterinario.Data.Repositories.PuestoRepository>();

// Registrar tus Servicios
builder.Services.AddScoped<HospitalVeterinario.Services.IEmpleadoService, HospitalVeterinario.Services.EmpleadoService>();
builder.Services.AddScoped<HospitalVeterinario.Services.IDepartamentoService, HospitalVeterinario.Services.DepartamentoService>();
builder.Services.AddScoped<HospitalVeterinario.Services.IPuestoService, HospitalVeterinario.Services.PuestoService>();
builder.Services.AddScoped<INominaService, NominaService>();

builder.Services.AddScoped<
    HospitalVeterinario.Data.Repositories.IUserRepository,
    HospitalVeterinario.Data.Repositories.UserRepository>();

builder.Services.AddScoped<
    HospitalVeterinario.Services.IUserService,
    HospitalVeterinario.Services.UserService>();
var app = builder.Build();

// --- INICIO DE CÓDIGO DE DIAGNÓSTICO DE CONTROLADORES ---
Console.WriteLine();
Console.WriteLine("--- DIAGNÓSTICO DE CONTROLADORES ---");
try
{
    var manager = app.Services.GetRequiredService<ApplicationPartManager>();
    var feature = new ControllerFeature();
    manager.PopulateFeature(feature);

    Console.WriteLine($"Número de controladores descubiertos: {feature.Controllers.Count}");
    foreach (var controllerTypeInfo in feature.Controllers)
    {
        Console.WriteLine($"Controlador Descubierto: {controllerTypeInfo.FullName}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR AL OBTENER CARACTERÍSTICAS DE CONTROLADOR: {ex.Message}");
    Console.WriteLine(ex.ToString());
}
Console.WriteLine("--- FIN DE DIAGNÓSTICO DE CONTROLADORES ---");
Console.WriteLine();
// --- FIN DE CÓDIGO DE DIAGNÓSTICO DE CONTROLADORES ---

// Configure the HTTP request pipeline.
// Volvemos a agregar el pipeline más completo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.UseSwagger();
    //app.UseSwaggerUI();   Asegúrate que el path en UseSwaggerUI() sea el correcto si lo personalizaste
}

app.UseHttpsRedirection();


app.UseStaticFiles();


app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/diagtest", () => "¡Endpoint de diagnóstico /diagtest alcanzado exitosamente!");

// --- INICIO DE CÓDIGO DE DIAGNÓSTICO DE ENDPOINTS REGISTRADOS ---
Console.WriteLine();
Console.WriteLine("--- DIAGNÓSTICO DE ENDPOINTS REGISTRADOS ---");
try
{
    var endpointDataSources = app.Services.GetService<IEnumerable<EndpointDataSource>>();
    if (endpointDataSources != null && endpointDataSources.Any())
    {
        var allEndpoints = endpointDataSources.SelectMany(es => es.Endpoints).ToList();
        Console.WriteLine($"Número total de endpoints registrados: {allEndpoints.Count}");
        foreach (var endpoint in allEndpoints)
        {
            Console.WriteLine($"Endpoint DisplayName: {endpoint.DisplayName}");
            if (endpoint is RouteEndpoint routeEndpoint)
            {
                Console.WriteLine($"  Ruta (RawText): {routeEndpoint.RoutePattern.RawText}");
            }
            Console.WriteLine("  ----");
        }
    }
    else { Console.WriteLine("No se pudieron obtener los EndpointDataSources o están vacíos."); }
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR AL OBTENER ENDPOINTS: {ex.Message}");
    Console.WriteLine(ex.ToString());
}
Console.WriteLine("--- FIN DE DIAGNÓSTICO DE ENDPOINTS REGISTRADOS ---");
Console.WriteLine();
// --- FIN DE CÓDIGO DE DIAGNÓSTICO DE ENDPOINTS REGISTRADOS ---

app.MapControllerRoute(
    name: "default",
  pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}