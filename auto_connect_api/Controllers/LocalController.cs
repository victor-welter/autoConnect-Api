using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly ILocalRepository _localRepository;

        public LocalController(ILocalRepository localRepository)
        {
            _localRepository = localRepository ?? throw new ArgumentNullException(nameof(localRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Local local)
        {
            try
            {
                _localRepository.Add(local);

                return Ok(new
                {
                    success = true,
                    data = "Local cadastrado com sucesso."
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

        [HttpPost("buscaLocais")]
        public ActionResult<List<Local>> BuscarTodosLocais(string? where = null)
        {
            try
            {
                dynamic data = _localRepository.Get(where);

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

        [HttpPost("DeleteLocal")]
        public IActionResult DeleteLocal(int id_local)
        {
            try
            {
                bool result = _localRepository.DeleteLocalByIdLocal(id_local);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Local não encontrado."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Local deletado com sucesso."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}
