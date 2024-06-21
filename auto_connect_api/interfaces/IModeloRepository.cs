using auto_connect_api.Models;
using System.Net.NetworkInformation;

namespace auto_connect_api.interfaces
{
    public interface IModeloRepository
    {
        void Add(Modelo modelo);

        List<Modelo> Get(string? where = null);

        Modelo GetByIdModelo(int id_modelo);

        bool DeleteModeloByIdModelo(int id_modelo);
    }
}
