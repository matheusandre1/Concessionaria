using agencia.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace agencia.Domain.DTOs
{
    public class AdminDTO
    {
        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Senha { get; set; }

        [Required]
        [StringLength(10)]
        public Perfil? Perfil { get; set; }
    }
}
