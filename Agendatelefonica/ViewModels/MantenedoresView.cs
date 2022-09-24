using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.ViewModels
{
    public class MantenedoresView
    {
        public int Id { get; set; }

        [Required]
        public string Mantenedor { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Funcion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Subsistema { get; set; }

        public string Extension { get; set; }

        public string RadioTetra { get; set; }
    }
}
