using auto_connect_api.interfaces;
using auto_connect_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IModeloRepository _modeloRepository;
        private readonly IMarcaRepository _marcaRepository;

        public VeiculoController(IVeiculoRepository veiculoRepository, IModeloRepository modeloRepository, IMarcaRepository marcaRepository)
        {
            _veiculoRepository = veiculoRepository ?? throw new ArgumentNullException(nameof(veiculoRepository));
            _modeloRepository = modeloRepository ?? throw new ArgumentNullException(nameof(modeloRepository));
            _marcaRepository = marcaRepository ?? throw new ArgumentNullException(nameof(marcaRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Veiculo veiculo)
        {
            try
            {
                _veiculoRepository.Add(veiculo);

                return Ok(new
                {
                    success = true,
                    data = "Veículo cadastrado com sucesso."
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

        [HttpPost("buscaVeiculos")]
        public ActionResult<List<Veiculo>> BuscarTodosVeiculos(string? usuario_cpf_cnpj = null, string? where = null)
        {
            try
            {
                dynamic data = _veiculoRepository.Get(usuario_cpf_cnpj, where);

                foreach (var veiculo in data) {
                    veiculo.Modelo = _modeloRepository.GetByIdModelo(veiculo.id_modelo);
                    veiculo.Marca = _marcaRepository.GetByIdMarca(veiculo.id_marca);
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

        [HttpPost("DeleteVeiculo")]
        public IActionResult DeleteVeiculo(int id_veiculo)
        {
            try
            {
                bool result = _veiculoRepository.DeleteVeiculoByIdVeiculo(id_veiculo);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Veículo não encontrado."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Veículo deletado com sucesso."
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
