using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class Recetas
    {
        [Key]
        public int idreceta { get; set; }
        public string RNombre { get; set; }
        public double Precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
