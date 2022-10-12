using System;
using System.Collections.Generic;

#nullable disable

namespace Agendatelefonica.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Codigo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public DateTime? Fecha { get; set; }

        public  Rol IdRolNavigation { get; set; }
    }
}
