using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public CategoriaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria)
        {
            _context.categoria.Add(categoria);
            _context.SaveChanges();
        }

        public List<Categoria> Get(string? where = null)
        {
            var baseQuery = "SELECT * FROM categoria WHERE 1=1";
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
            return _context.categoria.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public Categoria GetByIdCategoria(int id_categoria)
        {
            return _context.categoria.SingleOrDefault(m => m.id_categoria == id_categoria);
        }

        public bool DeleteCategoriaByIdCategoria(int id_categoria)
        {
            var categoria = _context.categoria.Find(id_categoria);
            if (categoria == null)
            {
                return false;
            }

            _context.categoria.Remove(categoria);
            _context.SaveChanges();
            return true;
        }
    }
}
