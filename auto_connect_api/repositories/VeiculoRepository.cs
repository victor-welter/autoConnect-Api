using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public VeiculoRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Veiculo veiculo)
        {
            _context.veiculo.Add(veiculo);
            _context.SaveChanges();
        }

        public List<Veiculo> Get(string? usuario_cpf_cnpj = null, string? where = null)
        {
            var baseQuery = @"SELECT v.id_veiculo, v.ano, v.placa, v.usuario_cpf_cnpj, 
                mo.id_modelo, mo.descricao, v.id_marca, v.odometro
                FROM veiculo v
                INNER JOIN modelo mo ON v.id_modelo = mo.id_modelo
                WHERE 1=1";

            var parameters = new List<object>();

            // Adicionando filtro por usuario_cpf_cnpj
            if (!string.IsNullOrWhiteSpace(usuario_cpf_cnpj))
            {
                baseQuery += " AND v.usuario_cpf_cnpj = {0}";
                parameters.Add(usuario_cpf_cnpj);
            }

            // Adicionando filtro por where
            if (!string.IsNullOrWhiteSpace(where))
            {
                var descricaoNormalized = where.ToLower().Normalize(NormalizationForm.FormD);
                baseQuery += " AND unaccent(lower(mo.descricao)) LIKE unaccent(lower({1}))";
                parameters.Add($"%{descricaoNormalized}%");
            }

            // Executando a query
            return _context.veiculo.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public Veiculo GetByIdVeiculo(int id_veiculo)
        {
            return _context.veiculo.SingleOrDefault(m => m.id_veiculo == id_veiculo);
        }

        public bool DeleteVeiculoByIdVeiculo(int id_veiculo)
        {
            var veiculo = _context.veiculo.Find(id_veiculo);
            if (veiculo == null)
            {
                return false;
            }

            _context.veiculo.Remove(veiculo);
            _context.SaveChanges();
            return true;
        }
    }
}
