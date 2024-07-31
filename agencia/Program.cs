using agencia.Domain.DTOs;
using agencia.Domain.Interfaces;
using agencia.Domain.Services;
using agencia.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAdminService, AdminServices>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", ([FromBody]LoginDTO loginDTO, IAdminService adminService) =>
{


    if (adminService.Login(loginDTO) != null)
    {
        return Results.Ok("Login com sucesso");
    }
    else
    {
        return Results.Unauthorized();
    }
});
app.Run();
