using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface ITipoCombustivelRepository
    {
        void Add(TipoCombustivel tipo_cobustivel);

        List<TipoCombustivel> Get(string? where = null);

        TipoCombustivel GetByIdTipoCombustivel(int id_tipo_combustivel);

        bool DeleteTipoCombustivelByIdTipoCombustivel(int id_tipo_combustivel);
    }
}
