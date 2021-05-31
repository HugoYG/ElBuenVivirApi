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
    public class PacienteNRController : ControllerBase
    {
        private readonly AppDbContext context;

        public PacienteNRController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var pacientes = (from u in context.PacientesNR
                             select new 
                             {
                                 Nombre = u.NRNombre,
                                 DUI = u.DUI,
                                 Telefono = u.Telefono,
                                 Direccion = u.Direccion
                             });
            return pacientes;
        }

        [HttpPost]
        public async Task<ActionResult<PacienteNR>> Post([FromBody] PacienteNR paciente)
        {
            context.PacientesNR.Add(paciente);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), paciente);
        }

    }
}
