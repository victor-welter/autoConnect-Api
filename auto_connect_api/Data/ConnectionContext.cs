using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;

namespace auto_connect_api.Data
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Veiculo> veiculo { get; set; }
        public DbSet<Marca> marca { get; set; }
        public DbSet<Modelo> modelo { get; set; }
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Local> local { get; set; }
        public DbSet<TipoCombustivel> tipo_combustivel { get; set; }
        public DbSet<TipoDespesa> tipo_despesa { get; set; }
        public DbSet<TipoProblema> tipo_problema { get; set; }
        public DbSet<Despesa> despesa { get; set; }
        public DbSet<Notificacao> notificacao { get; set; }
    }
}
