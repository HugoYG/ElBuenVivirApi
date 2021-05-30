using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class Medico
    {
        [Key]
        public int id_Medico { get; set; }
        public string MNombre { get; set; }
        public string DUI { get; set; }
        public string Area_Especialidad { get; set; }
        public string Horarios_Consulta { get; set; }
        public string Dias_Laborales { get; set; }
    }
}
