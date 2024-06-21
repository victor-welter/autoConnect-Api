using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);

        List<Usuario> Get();

        Usuario GetByCpfCnpj(string cpf_cnpj);
    }
}
