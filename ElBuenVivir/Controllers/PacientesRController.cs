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
    public class PacientesRController : ControllerBase
    {
        private readonly AppDbContext context;

        public PacientesRController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var pacientes = (from u in context.PacienteR
                             select new
                             {
                                 id = u.idPaciente,
                                 Nombre = u.PNombre,
                                 DUI = u.DUI,
                                 Telefono = u.Telefono,
                                 Direccion = u.Direccion
                             });
            return pacientes;
        }

        [HttpPost]
        public async Task<ActionResult<PacientesR>> Post([FromBody] PacientesR pacientes)
        {
            context.PacienteR.Add(pacientes);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), pacientes);
        }

    }
}
