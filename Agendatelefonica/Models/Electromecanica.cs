using System;
using System.Collections.Generic;

#nullable disable

namespace Agendatelefonica.Models
{
    public partial class Electromecanica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Extension { get; set; }
        public string Subsistema { get; set; }
    }
}
