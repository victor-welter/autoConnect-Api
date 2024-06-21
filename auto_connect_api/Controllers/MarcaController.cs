using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaController(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository ?? throw new ArgumentNullException(nameof(marcaRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Marca marca)
        {
            try
            {
                _marcaRepository.Add(marca);

                return Ok(new
                {
                    success = true,
                    data = "Marca cadastrada com sucesso."
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

        [HttpPost("buscaMarcas")]
        public ActionResult<List<Marca>> BuscarTodasMarcas(string? where = null)
        {
            try
            {
                dynamic data = _marcaRepository.Get(where);

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

        [HttpPost("DeleteMarca")]
        public IActionResult DeleteMarca(int id_marca)
        {
            try
            {
                bool result = _marcaRepository.DeleteMarcaByIdMarca(id_marca);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Marca não encontrada."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Marca deletada com sucesso."
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
