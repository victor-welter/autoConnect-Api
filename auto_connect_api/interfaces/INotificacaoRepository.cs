using auto_connect_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.interfaces
{
    public interface INotificacaoRepository
    {
        void Add(Notificacao notificacao);

        List<Notificacao> GetByUsuario(string? usuario_cpf_cnpj = null);
    }
}
