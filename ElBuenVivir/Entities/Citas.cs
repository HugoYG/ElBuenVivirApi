using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class Citas
    {
        [Key]
        public int id_Cita { get; set; }
        public string Fecha { get; set; }
        public int id_Paciente  { get; set; }
        public int idMedico { get; set; }
        public string Sntomas { get; set; }

        public bool Registered { get; set; }

        [NotMapped]
        public string Dui_P { get; set; }
        [NotMapped]
        public string Dui_D { get; set; }
    }
}
