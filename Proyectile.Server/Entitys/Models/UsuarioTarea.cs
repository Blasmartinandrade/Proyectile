using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Proyectile.Server.Entitys.Models
{
    public class UsuarioTarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }

       
        [ForeignKey("TareaID")]
        public int TareaID { get; set; }


        public void setIdUsuario(int id)
        {
            this.UsuarioID = id;
        }

        public void setIdTarea(int id)
        {
            this.TareaID = id;
        }
    }
}
