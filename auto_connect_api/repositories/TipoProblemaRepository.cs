using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class TipoProblemaRepository : ITipoProblemaRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public TipoProblemaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(TipoProblema tipo_problema)
        {
            _context.tipo_problema.Add(tipo_problema);
            _context.SaveChanges();
        }

        public List<TipoProblema> Get(string? where = null)
        {
            var baseQuery = "SELECT * FROM tipos_problema WHERE 1=1";
            var whereNormalized = "";

            // Adicionando filtro por descrição
            if (!string.IsNullOrWhiteSpace(where))
            {
                whereNormalized = where.ToLower().Normalize(NormalizationForm.FormD);
                baseQuery += " AND unaccent(lower(descricao)) LIKE unaccent(lower({0}))";
            }

            // Montando os parâmetros
            var parameters = new List<object>();
            if (!string.IsNullOrWhiteSpace(where)) parameters.Add($"%{whereNormalized}%");

            // Executando a query
            return _context.tipo_problema.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public TipoProblema GetByIdTipoProblema(int id_tipo_problema)
        {
            return _context.tipo_problema.SingleOrDefault(m => m.id_tipo_problema == id_tipo_problema);
        }

        public bool DeleteTipoProblemaByIdTipoProblema(int id_tipo_problema)
        {
            var tipo_problema = _context.tipo_problema.Find(id_tipo_problema);
            if (tipo_problema == null)
            {
                return false;
            }

            _context.tipo_problema.Remove(tipo_problema);
            _context.SaveChanges();
            return true;
        }
    }
}
