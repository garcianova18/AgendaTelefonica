using System.ComponentModel.DataAnnotations;

namespace Agendatelefonica.ViewModels
{
    public class UsuarioCreateView
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int IdRol { get; set; }

      

       

    }
}
