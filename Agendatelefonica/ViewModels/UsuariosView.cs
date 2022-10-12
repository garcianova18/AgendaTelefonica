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

        public Rol IdRolNavigation { get; set; }

        //con esta propiedad compuesta por la prop, de navegacion y la propiead nombre de la tabla ROl
        // mapper la que hace es ir a la tabla ROl y traerme el nombre
        public  string IdRolNavigationNombre { get; set; }

        


    }
}
