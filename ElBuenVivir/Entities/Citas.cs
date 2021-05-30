using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class Citas
    {
        [Key]
        public int id_Cita { get; set; }
        public DateTime Fecha { get; set; }
        public int id_Paciente  { get; set; }
        public int idMedico { get; set; }
        public string Sntomas { get; set; }
    }
}
