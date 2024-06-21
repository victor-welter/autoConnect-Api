using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface ICategoriaRepository
    {
        void Add(Categoria categoria);

        List<Categoria> Get(string? where = null);

        Categoria GetByIdCategoria(int id_categoria);

        bool DeleteCategoriaByIdCategoria(int id_categoria);
    }
}
