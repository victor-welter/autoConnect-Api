using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface ITipoProblemaRepository
    {
        void Add(TipoProblema tipo_problema);

        List<TipoProblema> Get(string? where = null);

        TipoProblema GetByIdTipoProblema(int id_tipo_despesa);

        bool DeleteTipoProblemaByIdTipoProblema(int id_tipo_despesa);
    }
}
