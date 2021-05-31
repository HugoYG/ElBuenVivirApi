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
    public class HistorialMedicoController : ControllerBase
    {
        private readonly AppDbContext context;

        public HistorialMedicoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var historial = (from c in context.Citas
                             join u in context.PacientesR on c.id_Paciente equals u.idPaciente
                             join m in context.Medico on c.idMedico equals m.id_Medico
                             where c.Registered == true
                             select new
                             {
                                 id_cita = c.id_Cita,
                                 id_paciente = u.idPaciente,
                                 nombre_Paciente = u.PNombre,
                                 id_medico = m.id_Medico,
                                 nombre_Medico = m.MNombre,
                                 Sintomas = c.Sntomas,
                                 Fecha = c.Fecha
                             });
            return historial;
        }


        [HttpGet("{id}")]
        public IQueryable GetFilteredbyUser(int id)
        {
            var historial = (from c in context.Citas
                             join u in context.PacientesR on c.id_Paciente equals u.idPaciente
                             join m in context.Medico on c.idMedico equals m.id_Medico
                             where u.idPaciente == id
                             select new
                             {
                                 id_cita = c.id_Cita,
                                 id_paciente = u.idPaciente,
                                 nombre_Paciente = u.PNombre,
                                 id_medico = m.id_Medico,
                                 nombre_Medico = m.MNombre,
                                 Sintomas = c.Sntomas,
                                 Fecha = c.Fecha
                             });
            return historial;
        }

        [HttpPost]
        public async Task<ActionResult<HistorialMedico>> Post([FromBody] HistorialMedico historial)
        {
            context.Historial_Medico.Add(historial);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), historial);
        }

    }
}
