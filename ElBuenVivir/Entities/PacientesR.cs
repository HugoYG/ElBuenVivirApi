using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class PacientesR
    {
        [Key]
        public int idPaciente { get; set; }
        public string PNombre { get; set; }
        public string DUI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
