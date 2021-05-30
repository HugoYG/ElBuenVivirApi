using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class HistorialMedico
    {
        [Key]
        public int id { get; set; }
        public int idCita { get; set; }
        public int id_paciente { get; set; }
        public int id_receta { get; set; }
    }
}
