using Microsoft.EntityFrameworkCore;
using Proyectile.Server.Entitys.Models;

namespace Proyectile.Server.Entitys.Services
{
    public class ProyectoServices
    {
        private readonly DataContext _context;
        public ProyectoServices(DataContext _context)
        {
            this._context = _context;
        }


        public async Task<ICollection<Proyecto>> ListaProyectos()
        {
            return await _context.Proyectos.ToListAsync();
        }


        public async Task<Proyecto?> CrearProyecto(Proyecto? nuevoProyecto, Usuario user)
        {
            if (nuevoProyecto != null && user != null)
            {
                await _context.Proyectos.AddAsync(nuevoProyecto);
                await _context.SaveChangesAsync();

                // Buscar proyecto creado para obtener su ID
                var proyectoCreado = await _context.Proyectos.FirstOrDefaultAsync(p => p.Token == nuevoProyecto.Token);

                if (proyectoCreado != null)
                {
                    UsuarioProyecto up = new UsuarioProyecto();
                    up.setIdProyecto(proyectoCreado.getId());
                    up.setIdUsuario(user.getId());
                    await _context.UsuariosProyectos.AddAsync(up);
                    await _context.SaveChangesAsync();

                    return proyectoCreado;
                }
                else
                {
                    return null; // Token inválido
                }
            }

            return null; // Error al cargar proyecto (Proyecto o Usuario no existente)
        }

        public async Task<Proyecto?> RecuperarProyectoId(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {

                proyecto = await ObtenerObjetivos(proyecto);

                return proyecto;
            }
            return null;

        }

        public async Task<Proyecto> ModificarProyecto(Proyecto proyecto)
        {
            if(proyecto != null)
            {
                var p_aux = await RecuperarProyectoId(proyecto.getId());
                if(p_aux != null)
                {
                    p_aux.setNombre(proyecto.getNombre());
                    p_aux.setImagen(proyecto.getImagen());
                    p_aux.setDescripcion(proyecto.getDescripcion());

                    await _context.SaveChangesAsync();

                    return p_aux;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task EliminarProyecto(Proyecto proyecto)
        {
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);


                List<UsuarioProyecto> up = await _context.UsuariosProyectos
                .Where(upp => upp.ProyectoID == proyecto.Id)
                .ToListAsync();
                if (up != null)
                {
                    for (int h = 0; h < up.Count; h++)
                    {
                        if (up[h] != null)
                        {
                            _context.UsuariosProyectos.Remove(up[h]);
                        }
                    }

                }

                List<Objetivo> objetivos = await _context.Objetivos
                .Where(o => o.ProyectoID == proyecto.Id)
                .ToListAsync();
                if (objetivos != null)
                {
                    for (int i = 0; i < objetivos.Count; i++)
                    {
                        if (objetivos[i] != null)
                        {
                            _context.Objetivos.Remove(objetivos[i]);


                            List<Tarea> tareas = await _context.Tareas
                            .Where(t => t.ObjetivoID == objetivos[i].Id)
                            .ToListAsync();
                            if (tareas != null)
                            {
                                for (int j = 0; j < tareas.Count; j++)
                                {
                                    if (tareas[j] != null)
                                    {
                                        _context.Tareas.Remove(tareas[j]);


                                        List<UsuarioTarea> ut = await _context.UsuariosTareas
                                        .Where(utt => utt.TareaID == tareas[j].Id)
                                        .ToListAsync();
                                        if (ut != null)
                                        {
                                            for (int k = 0; k < ut.Count; k++)
                                            {
                                                if (ut[k] != null)
                                                {
                                                    _context.UsuariosTareas.Remove(ut[k]);
                                                }
                                            }
                                        }


                                    }

                                }
                            }


                        }

                    }

                }





                await _context.SaveChangesAsync();
            }
        }



        public async Task<Proyecto?> ObtenerObjetivos(Proyecto? proyecto)
        {

            if (proyecto != null)
            {

                var objetivos = await _context.Objetivos
                .Where(o => o.ProyectoID == proyecto.Id)
                .ToListAsync();

                for (int i = 0; i < objetivos.Count; i++)
                {
                    if (objetivos[i] != null)
                    {
                        objetivos[i].Tareas = await ObtenerTareas(objetivos[i]);
                        objetivos[i].setEstado();
                        proyecto.Objetivos.Add(objetivos[i]);

                    }
                }

                return proyecto;

            }

            return null;

        }


        public async Task<List<Tarea>?> ObtenerTareas(Objetivo o)
        {

            if (o != null)
            {

                var tareas = await _context.Tareas
                .Where(t => t.ObjetivoID == o.Id)
                .ToListAsync();

                for (int i = 0; i < tareas.Count; i++)
                {
                    if (tareas[i] != null)
                    {
                        o.Tareas.Add(tareas[i]);
                    }
                }

            }

            return null;
        }
    }
}
