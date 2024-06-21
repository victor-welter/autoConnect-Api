using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface IDespesaRepository
    {
        void Add(Despesa despesa);

        List<Despesa> Get(string? usuario_cpf_cnpj = null, int? id_veiculo = null, DateTime? startDate = null, DateTime? endDate = null);

        double GetAverageDespesas(DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null);

        double GetTotalDespesas(DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null);

        double GetTotalDespesasByIdTipoDespesa(int id_tipo_despesa, DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null);

        bool DeleteDespesaByIdDespesa(int id_despesa);

        
    }
}
