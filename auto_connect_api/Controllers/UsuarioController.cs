using auto_connect_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace auto_connect_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult<List<UsuarioModel>> BuscarTodosUsuarios() 
        {
            return Ok();
        }
    }
}
