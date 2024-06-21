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
            try
            {
                _usuarioRepository.Add(usuario);

                return Ok(new
                {
                    success = true,
                    data = "Usuário cadastrado com sucesso."
                });
            } 
            catch(Exception ex) 
            {
                return Ok(new
                {
                    success = false,
                    error = ex.Message,
                });
            }
        }

        [HttpGet("buscaUsuarios")]
        public ActionResult<List<Usuario>> BuscarTodosUsuarios() 
        {
            try 
            {
                dynamic data = _usuarioRepository.Get();

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

        [HttpPost("login")]
        public IActionResult Login(string cpf_cnpj, string senha)
        {
            try 
            { 
                dynamic usuario = _usuarioRepository.GetByCpfCnpj(cpf_cnpj);
                if (usuario == null)
                {
                    return Ok(new
                    {
                        success = false,
                        error = "Usuário não encontrado.",
                    });
                }

                if (usuario.senha != senha)
                {
                    return Ok(new
                    {
                        success = false,
                        error = "CPF/CNPJ ou senha incorretos.",
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = usuario,
                });
            } 
            catch(Exception ex) 
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
