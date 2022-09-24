using Agendatelefonica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.ViewModels
{
    public class UsuarioView
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string FullName { get { return Nombre + Apellido; } }
        public string Codigo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
    }
}
