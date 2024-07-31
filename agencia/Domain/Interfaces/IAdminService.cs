using agencia.Domain.DTOs;
using agencia.Domain.Entities;

namespace agencia.Domain.Interfaces
{
    public interface IAdminService
    {
        Admin? Login(LoginDTO loginDTO);


    }
}
