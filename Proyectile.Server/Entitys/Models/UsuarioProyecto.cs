using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Proyectile.Server.Entitys.Models
{
    public class UsuarioProyecto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }

        [ForeignKey("ProyectoID")]
        public int ProyectoID { get; set; }


        public void setIdUsuario(int id)
        {
            this.UsuarioID = id;
        }

        public void setIdProyecto(int id)
        {
            this.ProyectoID = id;
        }
    }
}
