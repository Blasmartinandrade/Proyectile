using Microsoft.AspNetCore.Mvc;
using Proyectile.Server.Entitys.Models;
using Proyectile.Server.Entitys.Services;

namespace Proyectile.Server.Controllers
{

    [ApiController]
    [Route("api/tarea")]
    public class TareaController : Controller
    {
        private readonly TareaServices _service;
        public TareaController(TareaServices service)
        {
            _service = service;

        }


        public class CrearTareaRequest
        {
            public Objetivo Objetivo { get; set; }
            public Tarea Tarea { get; set; }
            public Usuario Usuario { get; set; }
        }

        [HttpPost("createtask")]
        public async Task<IActionResult> CrearTarea([FromBody] CrearTareaRequest request)
        {
            Objetivo obj = request.Objetivo;
            Tarea tarea = request.Tarea;
            Usuario user = request.Usuario;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            await _service.CrearTarea(obj, tarea, user);

            return StatusCode(201);
        }

        [HttpDelete("deletetask")]
        public async Task<IActionResult> EliminarTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            if(tarea != null)
            {
                await _service.EliminarTarea(tarea);
                return NoContent();
            }            

            return NotFound();
        }

        [HttpPut("updatetask")]
        public async Task<IActionResult> ModificarTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            if (tarea != null)
            {
                await _service.ModificarTarea(tarea);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
