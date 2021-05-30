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
            var historial = (from h in context.Historial_Medico
                             join u in context.PacienteR on h.id_paciente equals u.idPaciente
                             join r in context.Recetas on h.id_receta equals r.idreceta
                             join c in context.Citas on h.idCita equals c.id_Cita
                             join m in context.Medico on c.idMedico equals m.id_Medico
                             select new
                             {
                                 id_cita = h.idCita,
                                 id_paciente = h.id_paciente,
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
