using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCombustivelController : ControllerBase
    {
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;

        public TipoCombustivelController(ITipoCombustivelRepository tipoCombustivelRepository)
        {
            _tipoCombustivelRepository = tipoCombustivelRepository ?? throw new ArgumentNullException(nameof(tipoCombustivelRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] TipoCombustivel tipo_combustivel)
        {
            try
            {
                _tipoCombustivelRepository.Add(tipo_combustivel);

                return Ok(new
                {
                    success = true,
                    data = "Tipo combustivel cadastrado com sucesso."
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

        [HttpPost("buscaTipoCombustivel")]
        public ActionResult<List<TipoCombustivel>> BuscarTodosTiposCombustiveis(string? where = null)
        {
            try
            {
                dynamic data = _tipoCombustivelRepository.Get(where);

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

        [HttpPost("DeleteTipoCombustivel")]
        public IActionResult DeleteTipoCombustivel(int id_tipo_combustivel)
        {
            try
            {
                bool result = _tipoCombustivelRepository.DeleteTipoCombustivelByIdTipoCombustivel(id_tipo_combustivel);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Tipo combustivel não encontrado."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Tipo combustivel deletado com sucesso."
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
