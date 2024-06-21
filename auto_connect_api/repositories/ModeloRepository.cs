using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class ModeloRepository : IModeloRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public ModeloRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Modelo modelo)
        {
            _context.modelo.Add(modelo);
            _context.SaveChanges();
        }

        public List<Modelo> Get(string? where = null)
        {
            var baseQuery = "SELECT m.id_modelo, m.descricao, m.id_marca FROM modelo m WHERE 1=1";
            var whereNormalized = "";

            // Adicionando filtro por descrição
            if (!string.IsNullOrWhiteSpace(where))
            {
                whereNormalized = where.ToLower().Normalize(NormalizationForm.FormD);
                baseQuery += " AND unaccent(lower(m.descricao)) LIKE unaccent(lower({0}))";
            }

            // Montando os parâmetros
            var parameters = new List<object>();
            if (!string.IsNullOrWhiteSpace(where)) parameters.Add($"%{whereNormalized}%");

            // Executando a query
            return _context.modelo.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public Modelo GetByIdModelo(int id_modelo)
        {
            return _context.modelo.SingleOrDefault(m => m.id_modelo == id_modelo);
        }

        public bool DeleteModeloByIdModelo(int id_modelo)
        {
            var modelo = _context.modelo.Find(id_modelo);
            if (modelo == null)
            {
                return false;
            }

            _context.modelo.Remove(modelo);
            _context.SaveChanges();
            return true;
        }
    }
}
