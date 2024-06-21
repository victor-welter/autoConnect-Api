using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface ILocalRepository
    {
        void Add(Local local);

        List<Local> Get(string? where = null);

        Local GetByIdLocal(int id_local);

        bool DeleteLocalByIdLocal(int id_local);
    }
}
