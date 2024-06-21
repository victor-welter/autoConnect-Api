using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly ILocalRepository _localRepository;
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;
        private readonly ITipoDespesaRepository _tipoDespesaRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ITipoProblemaRepository _tipoProblemaRepository;

        public DespesaController(IDespesaRepository despesaRepository, ILocalRepository localRepository, ITipoCombustivelRepository tipoCombustivelRepository, ITipoDespesaRepository tipoDespesaRepository, IVeiculoRepository veiculoRepository, ITipoProblemaRepository tipoProblemaRepository)
        {
            _despesaRepository = despesaRepository ?? throw new ArgumentNullException(nameof(despesaRepository));
            _localRepository = localRepository ?? throw new ArgumentNullException(nameof(localRepository));
            _tipoCombustivelRepository = tipoCombustivelRepository ?? throw new ArgumentException(nameof(tipoCombustivelRepository));
            _tipoDespesaRepository = tipoDespesaRepository ?? throw new ArgumentException(nameof(tipoDespesaRepository));
            _veiculoRepository = veiculoRepository ?? throw new ArgumentException(nameof(veiculoRepository));
            _tipoProblemaRepository = tipoProblemaRepository ?? throw new ArgumentException(nameof(tipoProblemaRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Despesa despesa)
        {
            try
            {
                _despesaRepository.Add(despesa);

                return Ok(new
                {
                    success = true,
                    data = "Despesa cadastrada com sucesso."
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

        [HttpPost("buscaDespesas")]
        public ActionResult<List<Despesa>> BuscarTodasDespesas(string? usuario_cpf_cnpj = null, int? id_veiculo = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                dynamic data = _despesaRepository.Get(usuario_cpf_cnpj, id_veiculo, startDate, endDate);

                foreach (var despesa in data)
                {
                    if(despesa.id_local != null) despesa.local = _localRepository.GetByIdLocal(despesa.id_local);
                    if(despesa.id_tipo_combustivel != null) despesa.tipo_combustivel = _tipoCombustivelRepository.GetByIdTipoCombustivel(despesa.id_tipo_combustivel);
                    if(despesa.id_tipo_despesa != null) despesa.tipo_despesa = _tipoDespesaRepository.GetByIdTipoDespesa(despesa.id_tipo_despesa);
                    if(despesa.id_veiculo != null) despesa.veiculo = _veiculoRepository.GetByIdVeiculo(despesa.id_veiculo);
                    if(despesa.id_tipo_problema != null) despesa.tipo_problema = _tipoProblemaRepository.GetByIdTipoProblema(despesa.id_tipo_problema);
                }

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

        [HttpPost("buscaTotalDespesas")]
        public IActionResult GetTotalDespesas(DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null)
        {
            try
            {
                double total = _despesaRepository.GetTotalDespesas(startDate, endDate, usuario_cpf_cnpj, id_veiculo);
                return Ok(new
                {
                    success = true,
                    data = total,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                });
            }
        }

        [HttpPost("buscaMediaDespesas")]
        public IActionResult GetAverageDespesas(DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null)
        {
            try
            {
                double average = _despesaRepository.GetAverageDespesas(startDate, endDate, usuario_cpf_cnpj, id_veiculo);
                return Ok(new
                {
                    success = true,
                    data = average,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                });
            }
        }

        [HttpPost("buscaTotalDespesaPorTipo")]
        public IActionResult GetTotalDespesasByTipo(int id_tipo_despesa, DateTime? startDate = null, DateTime? endDate = null, string usuario_cpf_cnpj = null, int? id_veiculo = null)
        {
            try
            {
                double total = _despesaRepository.GetTotalDespesasByIdTipoDespesa(id_tipo_despesa, startDate, endDate, usuario_cpf_cnpj, id_veiculo);
                return Ok(new
                {
                    success = true,
                    data = total,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                });
            }
        }

        [HttpPost("DeleteDespesa")]
        public IActionResult DeleteDespesa(int id_despesa)
        {
            try
            {
                bool result = _despesaRepository.DeleteDespesaByIdDespesa(id_despesa);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Despesa não encontrada."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Despesa deletada com sucesso."
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
