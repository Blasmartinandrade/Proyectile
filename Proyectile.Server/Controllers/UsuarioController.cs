using Microsoft.AspNetCore.Mvc;
using Proyectile.Server.Entitys.Models;
using Proyectile.Server.Entitys.Services;

namespace Proyectile.Server.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        //Inyeccion de dependencias

        private readonly UsuarioServices _service;

        public UsuarioController(UsuarioServices service)
        {
            _service = service;
        }

        [HttpPost("createuser")]
        public async Task<ActionResult<Usuario>> InsertarUsuario(Usuario nuevo_usuario)
        {
            // Valida el modelo antes de insertar
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var usuarioInsertado = await _service.CrearUsuario(nuevo_usuario);
            return StatusCode(201, usuarioInsertado);
            // Devuelve 201 (Created) con la ubicación del nuevo usuario
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> ModificarUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var usuarioModificado = await _service.ModificarUsuario(usuario);

            if (usuarioModificado == null)
            {
                return NotFound();
            }

            return Ok(usuarioModificado);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var usuario = await _service.TraerUsuarioId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            await _service.EliminarUsuario(usuario);

            return NoContent();
        }

        [HttpGet("getusercredential")]
        public async Task<IActionResult> GetUserCredential(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var usuario = await _service.BuscarUsuarioPorCredencialesAsync(email, password);

            if(usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var usuario = await _service.TraerUsuarioId(id);

            if(usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }

            return NoContent();

        }


        [HttpPost("asignproject")]
        public async Task<IActionResult> AsignProject(Usuario user, string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var u = _service.AsignarProyecto(user, token);
            if(u != null)
            {
                return Ok(u);
            }
            return NotFound();

        }

        public class ExitProjectRequest
        {
            public Usuario Usuario { get; set; }
            public Proyecto Proyecto { get; set; }
        }

        [HttpDelete("exitproject")]
        public async Task<IActionResult> ExitProject([FromBody] ExitProjectRequest request)
        {
            Usuario user = request.Usuario;
            Proyecto project = request.Proyecto;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }

            var u = await _service.DesasignarProyecto(user, project);
            if (u != null)
            {
                return Ok(u);
            }
            return NotFound();
        }



    }
}
