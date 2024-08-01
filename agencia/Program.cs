using agencia.Domain.DTOs;
using agencia.Domain.Entities;
using agencia.Domain.Interfaces;
using agencia.Domain.ModelViews;
using agencia.Domain.Services;
using agencia.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAdminService, AdminServices>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));
var app = builder.Build();

#region Home 
app.MapGet("/", () => Results.Json(new Home()));
#endregion

#region Admin
app.MapPost("/admin/login", ([FromBody]LoginDTO loginDTO, IAdminService adminService) =>
{


    if (adminService.Login(loginDTO) != null)
    {
        return Results.Ok("Login com sucesso");
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Admin");
#endregion

#region Vehicles
app.MapPost("/vehicles", ([FromBody] VehicleDTO vehicleDTO, IVehicleService vehicleServico ) =>
{
    var vehicle = new Vehicle
    {
        Nome = vehicleDTO.Nome,
        Marca = vehicleDTO.Marca,
        Ano = vehicleDTO.Ano
    };
    vehicleServico.Include(vehicle);

    return Results.Created($"/veiculo/{vehicle.Id}", vehicle);
   


});
#endregion

app.MapGet("/vehicles", ([FromQuery]int? pagina, IVehicleService vehicleServico) =>
{
    var vehicle = vehicleServico.All(pagina);

    return Results.Ok(vehicle);



}).WithTags("Vehicles");

app.MapGet("/vehicles/{id}", ([FromRoute] int id, IVehicleService vehicleServico) =>
{
    var vehicle = vehicleServico.FinById(id);

    if (vehicle == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(vehicle);



}).WithTags("Vehicles");

app.MapPut("/vehicles/{id}", ([FromRoute] int id, VehicleDTO vehicleDTO, IVehicleService vehicleServico) =>
{
    var vehicle = vehicleServico.FinById(id);

    if (vehicle == null)
    {
        return Results.NotFound();
    }

    vehicle.Nome = vehicleDTO.Nome;
    vehicle.Marca = vehicleDTO.Marca;
    vehicle.Ano = vehicleDTO.Ano;

    vehicleServico.Update(vehicle);

    return Results.Ok(vehicle);



}).WithTags("Vehicles");

app.MapDelete("/vehicles/{id}", ([FromRoute] int id, IVehicleService vehicleServico) =>
{
    var vehicle = vehicleServico.FinById(id);

    if (vehicle == null)
    {
        return Results.NotFound();    }

    

    vehicleServico.Delete(vehicle);

    return Results.NoContent();



}).WithTags("Vehicles");
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
