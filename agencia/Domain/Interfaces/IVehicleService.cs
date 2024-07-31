using agencia.Domain.Entities;

namespace agencia.Domain.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> All(int pagina = 1, string? nome = null, string? marca = null);

        Vehicle? FinById (int id);

        void Include(Vehicle vehicle);

        void Update (Vehicle vehicle);

        public void Delete (Vehicle vehicle);
    }
}
