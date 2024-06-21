using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface IMarcaRepository
    {
        void Add(Marca marca);

        List<Marca> Get(string? where = null);

        Marca GetByIdMarca(int id_marca);

        bool DeleteMarcaByIdMarca(int id_marca);
    }
}
