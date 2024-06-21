using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace auto_connect_api.repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public DespesaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Despesa despesa)
        {
            _context.despesa.Add(despesa);
            _context.SaveChanges();
        }

        public List<Despesa> Get(string? usuario_cpf_cnpj = null, int? id_veiculo = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var baseQuery = @"SELECT d.id_despesa, d.data, d.id_veiculo, d.id_local, d.id_tipo_despesa, d.id_tipo_combustivel, d.id_tipo_problema,
                d.odometro, d.preco_unitario, d.quantidade, d.preco_total, d.descricao, d.manutencao_preventiva 
                FROM despesa d
                INNER JOIN veiculo v on(v.id_veiculo = d.id_veiculo)
                WHERE 1=1";

            var parameters = new List<object>();
            int parameterIndex = 0;

            // Adicionando filtro por usuario_cpf_cnpj
            if (!string.IsNullOrWhiteSpace(usuario_cpf_cnpj))
            {
                baseQuery += $" AND v.usuario_cpf_cnpj = {{{parameterIndex}}}";
                parameters.Add(usuario_cpf_cnpj);
                parameterIndex++;
            }

            // Adicionando filtro por id_veiculo
            if (id_veiculo.HasValue)
            {
                baseQuery += $" AND d.id_veiculo = {{{parameterIndex}}}";
                parameters.Add(id_veiculo);
                parameterIndex++;
            }

            // Adicionando filtro por startDate
            if (startDate.HasValue)
            {
                baseQuery += $" AND d.data >= {{{parameterIndex}}}";
                parameters.Add(startDate.Value);
                parameterIndex++;
            }

            // Adicionando filtro por endDate
            if (endDate.HasValue)
            {
                baseQuery += $" AND d.data <= {{{parameterIndex}}}";
                parameters.Add(endDate.Value);
                parameterIndex++;
            }


            // Executando a query
            return _context.despesa.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }

        public double GetAverageDespesas(DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null)
        {
            // Inicializa a consulta sem filtro específico
            var query = _context.despesa.AsQueryable();

            // Adiciona filtro de data inicial, se fornecido
            if (startDate.HasValue)
            {
                query = query.Where(d => d.data >= startDate.Value);
            }

            // Adiciona filtro de data final, se fornecido
            if (endDate.HasValue)
            {
                query = query.Where(d => d.data <= endDate.Value);
            }

            // Adiciona filtro de usuário, se fornecido
            if (!string.IsNullOrEmpty(usuario_cpf_cnpj))
            {
                query = query.Where(d => d.veiculo.usuario_cpf_cnpj == usuario_cpf_cnpj);
            }

            // Adiciona filtro de veículo, se fornecido
            if (id_veiculo.HasValue)
            {
                query = query.Where(d => d.id_veiculo == id_veiculo.Value);
            }

            // Calcula e retorna a média dos preços totais das despesas filtradas
            return query.Any() ? query.Average(d => d.preco_total) : 0.0;
        }

        public double GetTotalDespesas(DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null)
        {
            var query = _context.despesa.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(d => d.data >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(d => d.data <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(usuario_cpf_cnpj))
            {
                query = query.Where(d => d.veiculo.usuario_cpf_cnpj == usuario_cpf_cnpj);
            }

            if (id_veiculo.HasValue)
            {
                query = query.Where(d => d.id_veiculo == id_veiculo.Value);
            }

            return query.Sum(d => d.preco_total);
        }

        public double GetTotalDespesasByIdTipoDespesa(int id_tipo_despesa, DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null)
        {
            var query = _context.despesa
                                .Where(d => d.id_tipo_despesa == id_tipo_despesa);

            if (startDate.HasValue)
            {
                query = query.Where(d => d.data >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(d => d.data <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(usuario_cpf_cnpj))
            {
                query = query.Where(d => d.veiculo.usuario_cpf_cnpj == usuario_cpf_cnpj);
            }

            if (id_veiculo.HasValue)
            {
                query = query.Where(d => d.id_veiculo == id_veiculo.Value);
            }

            return query.Sum(d => d.preco_total);
        }

        public bool DeleteDespesaByIdDespesa(int id_despesa)
        {
            var despesa = _context.despesa.Find(id_despesa);
            if (despesa == null)
            {
                return false;
            }

            _context.despesa.Remove(despesa);
            _context.SaveChanges();
            return true;
        }
    }
}
