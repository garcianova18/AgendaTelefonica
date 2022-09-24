using System;
using System.Collections.Generic;

#nullable disable

namespace Agendatelefonica.Models
{
    public partial class Mantenedore
    {
        public int Id { get; set; }
        public string Mantenedor { get; set; }
        public string Nombre { get; set; }
        public string Funcion { get; set; }
        public string Telefono { get; set; }
        public string Subsistema { get; set; }
        public string Extension { get; set; }
        public string RadioTetra { get; set; }
    }
}
