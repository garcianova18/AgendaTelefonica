using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.ViewModels
{
    public class ElectromecanicaView
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Extension { get; set; }

        [Required]
        public string Subsistema { get; set; }
    }
}
