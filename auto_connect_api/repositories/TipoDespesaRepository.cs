using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class TipoDespesaRepository : ITipoDespesaRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public TipoDespesaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(TipoDespesa tipo_despesa)
        {
            _context.tipo_despesa.Add(tipo_despesa);
            _context.SaveChanges();
        }

        public List<TipoDespesa> Get(string? where = null)
        {
            var baseQuery = "SELECT * FROM tipos_despesa WHERE 1=1";
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
            return _context.tipo_despesa.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public TipoDespesa GetByIdTipoDespesa(int id_tipo_despesa)
        {
            return _context.tipo_despesa.SingleOrDefault(m => m.id_tipo_despesa == id_tipo_despesa);
        }

        public bool DeleteTipoDespesaByIdTipoDespesa(int id_tipo_despesa)
        {
            var tipo_despesa = _context.tipo_despesa.Find(id_tipo_despesa);
            if (tipo_despesa == null)
            {
                return false;
            }

            _context.tipo_despesa.Remove(tipo_despesa);
            _context.SaveChanges();
            return true;
        }
    }
}
