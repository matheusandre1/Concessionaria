using agencia.Domain.DTOs;
using agencia.Domain.Entities;
using agencia.Domain.Interfaces;
using agencia.Domain.ModelViews;
using agencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace agencia.Domain.Services
{
    public class AdminServices : IAdminService
    {
        private readonly DataContext _context;
        public AdminServices(DataContext context)
        {
            _context = context;

        }
        public Admin? Login(LoginDTO loginDTO)
        {
            var adm = _context.Administradores.Where(x => x.Email == loginDTO.Email && x.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
            
                

        }

        List<Admin> IAdminService.BuscaTodos(int? pagina)
        {
            var query = _context.Administradores.AsQueryable();            

            int itensPorPagina = 10;
            if (pagina != null)
            {
                query = query.Skip((int)(pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }


            return query.ToList();
        }

        public Admin? Include(Admin admin)
        {
            _context.Administradores.Add(admin);
            _context.SaveChanges();

            return admin;
        }

         Admin? IAdminService.FinById(int id)
        {
            return _context.Administradores.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
