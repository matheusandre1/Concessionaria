using agencia.Domain.DTOs;
using agencia.Domain.Entities;
using agencia.Domain.Interfaces;
using agencia.Infrastructure.Data;

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
    }
}
