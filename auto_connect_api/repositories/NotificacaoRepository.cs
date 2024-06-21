using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace auto_connect_api.repositories
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly ConnectionContext _context;

        // Injeção de Dependência do ConnectionContext
        public NotificacaoRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Notificacao notificacao)
        {
            _context.notificacao.Add(notificacao);
            _context.SaveChanges();
        }

        public List<Notificacao> GetByUsuario(string? usuario_cpf_cnpj = null)
        {
            var baseQuery = @"select n.id_notificacao, n.titulo, n.descricao, 
                n.visualizada, n.data_notificacao, n.usuario_cpf_cnpj, n.id_despesa 
                from notificacao n
                WHERE 1=1";

            var parameters = new List<object>();
            int parameterIndex = 0;

            // Adicionando filtro por usuario_cpf_cnpj
            if (!string.IsNullOrWhiteSpace(usuario_cpf_cnpj))
            {
                baseQuery += $" AND n.usuario_cpf_cnpj = {{{parameterIndex}}}";
                parameters.Add(usuario_cpf_cnpj);
                parameterIndex++;
            }

            // Executando a query
            return _context.notificacao.FromSqlRaw(baseQuery, parameters.ToArray()).ToList();
        }
    }
}
