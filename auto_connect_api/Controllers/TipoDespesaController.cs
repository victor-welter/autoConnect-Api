using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDespesaController : ControllerBase
    {
        private readonly ITipoDespesaRepository _tipoDespesaRepository;

        public TipoDespesaController(ITipoDespesaRepository tipoDespesaRepository)
        {
            _tipoDespesaRepository = tipoDespesaRepository ?? throw new ArgumentNullException(nameof(tipoDespesaRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] TipoDespesa tipo_despesa)
        {
            try
            {
                _tipoDespesaRepository.Add(tipo_despesa);

                return Ok(new
                {
                    success = true,
                    data = "Tipo despesa cadastrada com sucesso."
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

        [HttpPost("buscaTipoDespesa")]
        public ActionResult<List<TipoCombustivel>> BuscarTodosTiposDespesa(string? where = null)
        {
            try
            {
                dynamic data = _tipoDespesaRepository.Get(where);

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

        [HttpPost("DeleteTipoDespesa")]
        public IActionResult DeleteTipoDespesa(int id_tipo_despesa)
        {
            try
            {
                bool result = _tipoDespesaRepository.DeleteTipoDespesaByIdTipoDespesa(id_tipo_despesa);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Tipo despesa não encontrada."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Tipo despesa deletada com sucesso."
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
