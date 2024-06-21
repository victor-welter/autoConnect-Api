using auto_connect_api.interfaces;
using auto_connect_api.Models;
using auto_connect_api.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
        }

        [HttpPost("registrar")]
        public IActionResult Add([FromBody] Categoria categoria)
        {
            try
            {
                _categoriaRepository.Add(categoria);

                return Ok(new
                {
                    success = true,
                    data = "Categoria cadastrada com sucesso."
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

        [HttpPost("buscaCategorias")]
        public ActionResult<List<Categoria>> BuscarTodasCategorias(string? where = null)
        {
            try
            {
                dynamic data = _categoriaRepository.Get(where);

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

        [HttpPost("DeleteCategoria")]
        public IActionResult DeleteCategoria(int id_categoria)
        {
            try
            {
                bool result = _categoriaRepository.DeleteCategoriaByIdCategoria(id_categoria);
                if (!result)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Categoria não encontrada."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = "Categoria deletada com sucesso."
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
