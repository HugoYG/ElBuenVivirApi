using ElBuenVivir.Context;
using ElBuenVivir.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly AppDbContext context;

        public CitasController(AppDbContext context)
        {
            this.context = context;
        }

        //GetAll
        [HttpGet]
        public IQueryable Get()
        {
            var citas = (from c in context.Citas
                         join u in context.PacienteR on c.id_Paciente equals u.idPaciente
                         join m in context.Medico on c.idMedico equals m.id_Medico
                         select new
                         {
                             id = c.id_Cita,
                             id_medico = c.idMedico,
                             Nombre_Medico = m.MNombre,
                             id_paciente = c.id_Paciente,
                             Nombre_Paciente = u.PNombre,
                             Fecha = c.Fecha,
                             Sintomas = c.Sntomas
                         });
            return citas;
        }

        [HttpPost]
        public async Task<ActionResult<Citas>> Post([FromBody] Citas cita)
        {
            context.Citas.Add(cita);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), cita);
        }

    }
}
