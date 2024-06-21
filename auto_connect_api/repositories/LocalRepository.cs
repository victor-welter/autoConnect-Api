using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public LocalRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Local local)
        {
            _context.local.Add(local);
            _context.SaveChanges();
        }

        public List<Local> Get(string? where = null)
        {
            var baseQuery = "SELECT * FROM local WHERE 1=1";
            var whereNormalized = "";

            // Adicionando filtro por descrição
            if (!string.IsNullOrWhiteSpace(where))
            {
                whereNormalized = where.ToLower().Normalize(NormalizationForm.FormD);
                baseQuery += " AND unaccent(lower(nome)) LIKE unaccent(lower({0}))";
            }

            // Montando os parâmetros
            var parameters = new List<object>();
            if (!string.IsNullOrWhiteSpace(where)) parameters.Add($"%{whereNormalized}%");

            // Executando a query
            return _context.local.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public Local GetByIdLocal(int id_local)
        {
            return _context.local.SingleOrDefault(m => m.id_local == id_local);
        }

        public bool DeleteLocalByIdLocal(int id_local)
        {
            var local = _context.local.Find(id_local);
            if (local == null)
            {
                return false;
            }

            _context.local.Remove(local);
            _context.SaveChanges();
            return true;
        }
    }
}
