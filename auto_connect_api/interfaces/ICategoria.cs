using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface ICategoria
    {
        void Add(Categoria categoria);

        List<Categoria> Get();
    }
}
