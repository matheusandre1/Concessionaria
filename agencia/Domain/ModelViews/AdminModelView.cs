using agencia.Domain.Enums;

namespace agencia.Domain.ModelViews
{
    public record AdminModelView
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Perfil { get; set; }
    }
}
