using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface ITipoDespesaRepository
    {
        void Add(TipoDespesa tipo_despesa);

        List<TipoDespesa> Get(string? where = null);

        TipoDespesa GetByIdTipoDespesa(int id_tipo_despesa);

        bool DeleteTipoDespesaByIdTipoDespesa(int id_tipo_despesa);
    }
}
