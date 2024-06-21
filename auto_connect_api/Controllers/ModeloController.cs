using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly IModeloRepository _modeloRepository;

        public ModeloController(IModeloRepository modeloRepository)
        {
            _modeloRepository = modeloRepository ?? throw new ArgumentNullException(nameof(modeloRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Modelo modelo)
        {
            try
            {
                _modeloRepository.Add(modelo);

                return Ok(new
                {
                    success = true,
                    data = "Modelo cadastrado com sucesso."
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

        [HttpPost("buscaModelos")]
        public ActionResult<List<Modelo>> BuscarTodosModelos(string? where = null)
        {
            try
            {
                dynamic data = _modeloRepository.Get(where);

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

        [HttpPost("DeleteModelo")]
        public IActionResult DeleteModelo(int id_modelo)
        {
            try
            {
                bool result = _modeloRepository.DeleteModeloByIdModelo(id_modelo);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Modelo não encontrado."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Modelo deletado com sucesso."
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
