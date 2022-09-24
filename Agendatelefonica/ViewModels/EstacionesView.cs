using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.ViewModels
{
    public class EstacionesView
    {
        public int Id { get; set; }

        [Required]
        public string Linea { get; set; }

        [Required]
        public string Estacion { get; set; }
        [Required]
        public string Boleteria { get; set; }
        [Required]
        public string CuartoControl { get; set; }
        public string CuartoCom { get; set; }
        public string Enclavamiento { get; set; }
        public string CuartoAtbt { get; set; }
        public string SubestacionTraccion { get; set; }
        public string CabinaAnden { get; set; }

        [Required]
        public string Interfono { get; set; }
        public string PstnEmergencia { get; set; }
    }
}
