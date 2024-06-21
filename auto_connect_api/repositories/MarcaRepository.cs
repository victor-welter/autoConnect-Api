using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public MarcaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Marca marca)
        {
            _context.marca.Add(marca);
            _context.SaveChanges();
        }

        public List<Marca> Get(string? where = null)
        {
            var baseQuery = "SELECT m.id_marca, m.descricao FROM marca m WHERE 1=1";
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
            return _context.marca.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public Marca GetByIdMarca(int id_marca)
        {
            return _context.marca.SingleOrDefault(m => m.id_marca == id_marca);
        }

        public bool DeleteMarcaByIdMarca(int id_marca)
        {
            var marca = _context.marca.Find(id_marca);
            if (marca == null)
            {
                return false;
            }

            _context.marca.Remove(marca);
            _context.SaveChanges();
            return true;
        }
    }
}
