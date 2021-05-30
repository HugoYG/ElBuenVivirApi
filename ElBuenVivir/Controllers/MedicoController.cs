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
    public class MedicoController : ControllerBase
    {
        private readonly AppDbContext context;

        public MedicoController(AppDbContext context)
        {
            this.context = context;
        }

        //GetAll
        public IQueryable Get()
        {
            var medicos = (from m in context.Medico
                          select new
                          {
                              id = m.id_Medico,
                              Nombre = m.MNombre,
                              DUI = m.DUI,
                              Especialidad = m.Area_Especialidad,
                              Horario = m.Horarios_Consulta,
                              Dias_laborales = m.Dias_Laborales
                          });
            return medicos;
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> Post([FromBody] Medico medico)
        {
            context.Medico.Add(medico);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), medico);
        }

    }
}
