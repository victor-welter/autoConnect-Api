using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProblemaController : ControllerBase
    {
        private readonly ITipoProblemaRepository _tipoProblemaRepository;

        public TipoProblemaController(ITipoProblemaRepository tipoProblemaRepository)
        {
            _tipoProblemaRepository = tipoProblemaRepository ?? throw new ArgumentNullException(nameof(tipoProblemaRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] TipoProblema tipo_problema)
        {
            try
            {
                _tipoProblemaRepository.Add(tipo_problema);

                return Ok(new
                {
                    success = true,
                    data = "Tipo problema cadastrado com sucesso."
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

        [HttpPost("buscaTipoProblema")]
        public ActionResult<List<TipoProblema>> BuscarTodosTiposProblema(string? where = null)
        {
            try
            {
                dynamic data = _tipoProblemaRepository.Get(where);

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

        [HttpPost("DeleteTipoProblema")]
        public IActionResult DeleteTipoProblema(int id_tipo_problema)
        {
            try
            {
                bool result = _tipoProblemaRepository.DeleteTipoProblemaByIdTipoProblema(id_tipo_problema);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Tipo problema não encontrado."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Tipo problema deletado com sucesso."
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
