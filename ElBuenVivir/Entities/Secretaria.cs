using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBuenVivir.Entities
{
    public class Secretaria
    {
        [Key]
        public int id_Secretaria { get; set; }
        public string S_Nombre { get; set; }
        public string S_Password { get; set; }
        public string S_Email { get; set; }
    }
}
