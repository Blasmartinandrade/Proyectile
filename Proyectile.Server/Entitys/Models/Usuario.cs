using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Proyectile.Server.Entitys.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nchar(50)")]
        [AllowNull]
        public string Nombre { get; set; }


        [Column(TypeName = "nchar(250)")]
        [AllowNull]
        public string Descripcion { get; set; }

        [Required]
        [AllowNull]
        [Column(TypeName = "nchar(100)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nchar(270)")]
        [AllowNull]
        public string Password { get; set; }

        [Column(TypeName = "nchar(120)")]
        [AllowNull]
        public string Imagen { get; set; }


        /*-----------------------------------------------------------------------------*/

        [NotMapped]
        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        [NotMapped]

        public List<Tarea> Tareas { get; set; } = new List<Tarea>();



        /*-----------------------------------------------------------------------------*/
        //Setters

        public void setId(int Id)
        {
            this.Id = Id;
        }

        public void setNombre(string Nombre)
        {
            this.Nombre = Nombre;
        }

        public void setImagen(string imagen)
        {
            this.Imagen = imagen;
        }

        public void setPassword(string cadena)
        {

            Random random = new Random(123);

            string t = "";
            for (int i = 0; i < cadena.Length; i++)
            {
                int n = random.Next(0, 9);
                t = t + n + cadena[i];
            }
            this.Password = t;

        }

        public void setEmail(string Email)
        {
            this.Email = Email;
        }

        public void setDescripcion(string Desc)
        {
            this.Descripcion = Desc;
        }

        /*-----------------------------------------------------------------------------*/
        //GETTERS

        public int getId()
        {
            return this.Id;
        }

        public string getNombre()
        {
            return this.Nombre;
        }

        public string getPassword()
        {
            return this.Password;
        }

        public string getImagen()
        {
            return this.Imagen;
        }

        public List<Proyecto> getListProyectos()
        {

            return new List<Proyecto>();

        }

        public string getEmail()
        {
            return this.Email;
        }

        public string getDescripcion()
        {
            return this.Descripcion;
        }
    }
}
