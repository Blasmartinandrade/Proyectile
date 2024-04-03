using Microsoft.AspNetCore.Mvc;
using Proyectile.Server.Entitys.Services;
using Proyectile.Server.Entitys.Models;

namespace Proyectile.Server.Controllers
{
    [ApiController]
    [Route("api/objetivo")]
    public class ObjetivoController : Controller
    {
        private readonly ObjetivoServices _service;

        public ObjetivoController(ObjetivoServices service)
        {
            _service = service;
        }


        public class CrearObjetivoRequest
        {
            public Proyecto Proyecto { get; set; }
            public Objetivo Objetivo { get; set; }
        }

        [HttpPost("createobj")]
        public async Task<IActionResult> CrearObjetivo([FromBody] CrearObjetivoRequest request)
        {
            Proyecto pro = request.Proyecto;
            Objetivo obj = request.Objetivo;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }
            if (pro != null && obj != null)
            {
                await _service.CrearObjetivo(pro, obj);
                return StatusCode(201);
            }
            return BadRequest();
        }

        [HttpDelete("deleteobj")]
        public async Task<IActionResult> EliminarObjetivo(Objetivo obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }
            if(obj != null)
            {
                await _service.EliminarObjetivo(obj);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
