using agencia.Domain.Entities;
using agencia.Domain.Interfaces;
using agencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace agencia.Domain.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _context;
        public VehicleService(DataContext context)
        {
            _context = context;

        }

        public List<Vehicle> All(int pagina = 1, string? nome = null, string? marca = null)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {

                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
            }

            int itensPorPagina = 10;
            query = query.Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina) ;

            return query.ToList();
        }

        public void Delete(Vehicle vehicle)
        {
            _context.Remove(vehicle);
            _context.SaveChanges();
        }

        public Vehicle? FinById(int id)
        {
            return _context.Vehicles.Where(x=>x.Id == id).FirstOrDefault();
        }

        public void Include(Vehicle vehicle)
        {
           _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
            _context.Update(vehicle);
            _context.SaveChanges();
        }
    }
}
