using Proyectile.Server.Entitys.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Proyectile.Server.Entitys.Services
{
    public class TareaServices
    {
        private readonly DataContext _context;

        public TareaServices(DataContext ctx)
        {
            this._context = ctx;
        }

        public async Task CrearTarea(Objetivo obj, Tarea tarea, Usuario user)
        {
            if (obj != null && tarea != null && user != null)
            {
                var o_aux = await _context.Objetivos.FindAsync(obj.Id);
                var u_aux = await _context.Usuarios.FindAsync(user.Id);
                if (o_aux != null && u_aux != null)
                {
                    tarea.setObjetivoID(obj.getId());
                    tarea.Descompletar();
                    await _context.Tareas.AddAsync(tarea);

                    UsuarioTarea ut = new UsuarioTarea();
                    ut.setIdTarea(tarea.getId());
                    ut.setIdUsuario(user.getId());

                    await _context.UsuariosTareas.AddAsync(ut);
                    

                    await _context.SaveChangesAsync();
                }
                                
            }

        }

        public async Task EliminarTarea(Tarea? tarea)
        {
            if (tarea != null)
            {
                tarea = await _context.Tareas.FindAsync(tarea.Id);
                if (tarea != null)
                {
                    _context.Tareas.Remove(tarea);

                    var lista_ut = await _context.UsuariosTareas
                    .Where(ut => ut.TareaID == tarea.Id)
                    .ToListAsync();

                    if (lista_ut != null)
                    {
                        for (int i = 0; i < lista_ut.Count; i++)
                        {
                            if (lista_ut[i] != null)
                            {
                                _context.UsuariosTareas.Remove(lista_ut[i]);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }

            }
        }

        public async Task ModificarTarea(Tarea? tarea)
        {
            if (tarea != null)
            {
                var t_aux = await _context.Tareas.FindAsync(tarea.Id);
                if (t_aux != null)
                {
                    t_aux.Estado = tarea.getEstado();
                    t_aux.setNombre(tarea.getNombre());
                    await _context.SaveChangesAsync();
                }




            }
        }
    }
}
