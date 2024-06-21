using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;

namespace auto_connect_api.repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public UsuarioRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            _context.usuario.Add(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> Get()
        {
            return _context.usuario.ToList();
        }

        public Usuario GetByCpfCnpj(string cpf_cnpj)
        {
            return _context.usuario.FirstOrDefault(u => u.cpf_cnpj == cpf_cnpj);
        }
    }
}
