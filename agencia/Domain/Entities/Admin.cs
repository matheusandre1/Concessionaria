using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agencia.Domain.Entities
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Senha { get; set; }

        [StringLength(10)]
        public string Perfil { get; set; }
    }
}
