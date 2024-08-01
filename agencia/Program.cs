using agencia.Domain.DTOs;
using agencia.Domain.Entities;
using agencia.Domain.Enums;
using agencia.Domain.Interfaces;
using agencia.Domain.ModelViews;
using agencia.Domain.Services;
using agencia.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;


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
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
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

app.MapPost("/admin/", ([FromBody] AdminDTO adminDTO, IAdminService adminService) =>
{
    var validacao = new ErrorDeValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(adminDTO.Email))
    {
        validacao.Mensagens.Add("Campo não pode ser vazio");
    }

    if (string.IsNullOrEmpty(adminDTO.Senha))
    {
        validacao.Mensagens.Add("Campo não pode ser vazio");
    }

    if (adminDTO.Perfil == null)
    {
        validacao.Mensagens.Add("Campo não pode ser vazio");
    }

    if(validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }


    var admin = new Admin
    {
        Email = adminDTO.Email,
        Senha = adminDTO.Senha,
        Perfil = adminDTO.Perfil.ToString() ?? Perfil.Editor.ToString(),
    };

    adminService.Include(admin);

    return Results.Created($"/admin/{admin.Id}", new AdminModelView
    {
        Id = admin.Id,
        Email = admin.Email,
        Perfil = admin.Perfil
    });
}).WithTags("Admin");

app.MapGet("/admin/all", ([FromQuery] int? pagina, IAdminService adminService) =>
{
    var admin = new List<AdminModelView>();
    var busca = adminService.BuscaTodos(pagina);

    foreach ( var adm in busca)
    {
        admin.Add(new AdminModelView
        {
            Id = adm.Id,
            Email = adm.Email,
            Perfil = adm.Perfil
        });
    }
    return Results.Ok(admin);
}).WithTags("Admin");

app.MapGet("/admin/{id}", ([FromRoute] int id, IAdminService adminService) =>
{
    var admin= adminService.FinById(id);

    if (admin == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new AdminModelView
    {
        Id = admin.Id,
        Email = admin.Email,
        Perfil = admin.Perfil
    });



}).WithTags("Admin");

#endregion

#region 
ErrorDeValidacao validaDTO(VehicleDTO vehicleDTO)
{
    var validacao = new ErrorDeValidacao()
    {
        Mensagens = new List<string>()
    };
    if (string.IsNullOrEmpty(vehicleDTO.Nome))
    {
        validacao.Mensagens.Add("O nome não pode ser Vazio");
    }

    if (string.IsNullOrEmpty(vehicleDTO.Marca))
    {
        validacao.Mensagens.Add("O campo marca não pode ser Vazio");
    }
    if (vehicleDTO.Ano < 1950)
    {
        validacao.Mensagens.Add("Veiculo Muito Antigo");
    }

    return validacao;
};
app.MapPost("/vehicles", ([FromBody] VehicleDTO vehicleDTO, IVehicleService vehicleServico ) =>
{
   
    var validacao = validaDTO(vehicleDTO);    

    
    if(validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }
    var vehicle = new Vehicle
    {
        Nome = vehicleDTO.Nome,
        Marca = vehicleDTO.Marca,
        Ano = vehicleDTO.Ano
    };
    vehicleServico.Include(vehicle);

    return Results.Created($"/veiculo/{vehicle.Id}", vehicle);
   


}).WithTags("Vehicles");
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
    var validacao = validaDTO(vehicleDTO);


    if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
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
