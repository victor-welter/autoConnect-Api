using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;

namespace auto_connect_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<VeiculoModel> Veiculos { get; set; }
    }
}
