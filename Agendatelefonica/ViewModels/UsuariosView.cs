using Agendatelefonica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.ViewModels
{
    public class UsuariosView
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

        public string FullName { get; set; }

        public  string IdRolNavigationNombre { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
    }
}
