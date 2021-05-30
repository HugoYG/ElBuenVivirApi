using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class PacienteNR
    {
        [Key]
        public int id { get; set; }
        public string NRNombre { get; set; }
        public string DUI { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
