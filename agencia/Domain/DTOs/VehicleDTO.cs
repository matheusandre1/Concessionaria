namespace agencia.Domain.DTOs
{
    public record VehicleDTO
    {
        public string Nome { get; set; }

        public string Marca { get; set; }

        public int Ano {  get; set; }
    }
}
