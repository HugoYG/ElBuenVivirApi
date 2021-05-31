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
            var pacientes = (from u in context.PacientesR
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

        [HttpGet("secretaria/{id}")]
        public IQueryable GetBySecretaria(string id)
        {
            int _id;
            int.TryParse(id, out _id);
            var info = (from i in context.InfoAdmin
                        join s in context.Secretarias on i.id_Secretaria equals s.id_Secretaria
                        join m in context.Medico on i.id_Medico equals m.id_Medico
                        where s.id_Secretaria == _id
                        select new
                        {  
                            id_Medico = i.id_Medico,
                        });

            List<int> idMedicos = new List<int>();
            List<int> idPacientes = new List<int>();
            foreach (var item in info)
            {
                idMedicos.Add(item.id_Medico);
            }

            var citas = context.Citas.Where(s => idMedicos.Contains(s.idMedico));

            foreach (var paciente in citas)
            {
                idPacientes.Add(paciente.id_Paciente);
            }

            var pacientesFiltered = context.PacientesR.Where(p => idPacientes.Contains(p.idPaciente));
            return pacientesFiltered;
        }

        [HttpPost]
        public async Task<ActionResult<PacientesR>> Post([FromBody] PacientesR pacientes)
        {
            context.PacientesR.Add(pacientes);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), pacientes);
        }

    }
}
