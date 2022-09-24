using System;
using System.Collections.Generic;

#nullable disable

namespace Agendatelefonica.Models
{
    public partial class Estacione
    {
        public int Id { get; set; }
        public string Linea { get; set; }
        public string Estacion { get; set; }
        public string Boleteria { get; set; }
        public string CuartoControl { get; set; }
        public string CuartoCom { get; set; }
        public string Enclavamiento { get; set; }
        public string CuartoAtbt { get; set; }
        public string SubestacionTraccion { get; set; }
        public string CabinaAnden { get; set; }
        public string Interfono { get; set; }
        public string PstnEmergencia { get; set; }
    }
}
