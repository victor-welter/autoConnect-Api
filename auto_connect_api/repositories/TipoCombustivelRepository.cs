using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class TipoCombustivelRepository : ITipoCombustivelRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public TipoCombustivelRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(TipoCombustivel tipo_combustivel)
        {
            _context.tipo_combustivel.Add(tipo_combustivel);
            _context.SaveChanges();
        }

        public List<TipoCombustivel> Get(string? where = null)
        {
            var baseQuery = "SELECT * FROM tipo_combustivel WHERE 1=1";
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
            return _context.tipo_combustivel.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public TipoCombustivel GetByIdTipoCombustivel(int id_tipo_combustivel)
        {
            return _context.tipo_combustivel.SingleOrDefault(m => m.id_tipo_combustivel == id_tipo_combustivel);
        }

        public bool DeleteTipoCombustivelByIdTipoCombustivel(int id_tipo_combustivel)
        {
            var tipo_combustivel = _context.tipo_combustivel.Find(id_tipo_combustivel);
            if (tipo_combustivel == null)
            {
                return false;
            }

            _context.tipo_combustivel.Remove(tipo_combustivel);
            _context.SaveChanges();
            return true;
        }
    }
}
