using Microsoft.AspNetCore.Mvc;
using Proyectile.Server.Entitys.Services;
using Proyectile.Server.Entitys.Models;

namespace Proyectile.Server.Controllers
{
    [ApiController]
    [Route("api/proyecto")]
    public class ProyectoController : Controller
    {

        //Inyeccion de dependencias

        private readonly ProyectoServices _service;

        public ProyectoController(ProyectoServices service)
        {
            _service = service;
        }

        public class CrearProyectoRequest
        {
            public Proyecto Proyecto { get; set; }
            public Usuario User { get; set; }
        }

        [HttpPost("createproject")]
        public async Task<ActionResult<Proyecto>> CrearProyecto([FromBody] CrearProyectoRequest request)
        {
            Proyecto proyecto = request.Proyecto;
            Usuario user = request.User;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var p = await _service.CrearProyecto(proyecto, user);

            if (p != null)
            {
                return StatusCode(201, p);
            }
            else
            {
                return StatusCode(500, "Error en el servicio, p = null");
            }
        }

        [HttpDelete("deleteproject")]
        public async Task<IActionResult> EliminarProyecto(Proyecto project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }
            if(project == null)
            {
                return NotFound();
            }

            await _service.EliminarProyecto(project);
            return NoContent();

        }

        [HttpGet("getproject")]
        public async Task<IActionResult> ObtenerProyectoId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var p = await _service.RecuperarProyectoId(id);

            if(p != null)
            {
                return Ok(p);
            }

            else
            {
                return NotFound();
            }
        }

        [HttpPut("updateproject")]
        public async Task<IActionResult> ModificarProyecto(Proyecto proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }
            
            if (proyecto != null)
            {
                var p = await _service.ModificarProyecto(proyecto);
                if (p != null)
                {
                    return Ok(p);
                }
                else
                {
                    return NotFound();
                }
            }

            else
            {
                return NotFound();
            }
        }


    }
}
