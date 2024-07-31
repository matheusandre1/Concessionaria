using agencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace agencia.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin {
                    Id = 1,
                    Email = "administrador@teste.com",
                    Senha = "123456",
                    Perfil = "Adn"
                }
                );
               
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Opcionalmente, você pode adicionar uma string de conexão padrão aqui.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=consultoria;Username=postgres;Password=1234");
            }
        }

        // Defina suas entidades do banco de dados aqui. Exemplo:
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Admin> Administradores { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

    }
}
