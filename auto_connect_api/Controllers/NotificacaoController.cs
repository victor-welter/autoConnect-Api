using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        public NotificacaoController(INotificacaoRepository notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository ?? throw new ArgumentNullException(nameof(notificacaoRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Notificacao notificacao)
        {
            try
            {
                _notificacaoRepository.Add(notificacao);

                return Ok(new
                {
                    success = true,
                    data = "Notificação cadastrada com sucesso."
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    error = ex.Message,
                });
            }
        }

        [HttpPost("buscaNotificacao")]
        public ActionResult<List<Notificacao>> GetByUsuario(string? usuario_cpf_cnpj = null)
        {
            try
            {
                dynamic data = _notificacaoRepository.GetByUsuario(usuario_cpf_cnpj);

                return Ok(new
                {
                    success = true,
                    data = data,
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    error = ex.Message,
                });
            }
        }
    }
}
