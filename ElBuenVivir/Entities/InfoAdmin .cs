using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Entities
{
    public class InfoAdmin
    {
        [Key]
        public int id_Info { get; set; }
        public int id_Medico { get; set; }
        public int id_Secretaria { get; set; }
    }
}
