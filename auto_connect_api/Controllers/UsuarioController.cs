using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add(Usuario usuario) 
        {
            _usuarioRepository.Add(usuario);

            return Ok();
        }

        [HttpGet("buscaUsuarios")]
        public ActionResult<List<Usuario>> BuscarTodosUsuarios() 
        {
            dynamic data = _usuarioRepository.Get();

            return Ok(data);
        }

        [HttpPost("login")]
        public IActionResult Login(string cpf_cnpj, string senha)
        {
            dynamic usuario = _usuarioRepository.GetByCpfCnpj(cpf_cnpj);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (usuario.senha != senha)
            {
                return Unauthorized("CPF/CNPJ ou senha incorretos.");
            }

            return Ok("Login bem-sucedido.");
        }
    }
}
