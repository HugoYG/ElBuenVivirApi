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
                         join n in context.PacientesNR on c.id_Paciente equals n.id
                         join m in context.Medico on c.idMedico equals m.id_Medico
                         where c.Registered == false
                         select new
                         {

                             id = c.id_Cita,
                             id_medico = c.idMedico,
                             Nombre_Medico = m.MNombre,
                             id_paciente = c.id_Paciente,
                             Nombre_Paciente = n.NRNombre,
                             Dui_Paciente = n.DUI,
                             Fecha = c.Fecha,
                             Sintomas = c.Sntomas
                         });

            var _citas = (from c in context.Citas
                          join u in context.PacientesR on c.id_Paciente equals u.idPaciente
                          join m in context.Medico on c.idMedico equals m.id_Medico
                          where c.Registered == true
                          select new
                          {

                              id = c.id_Cita,
                              id_medico = c.idMedico,
                              Nombre_Medico = m.MNombre,
                              id_paciente = c.id_Paciente,
                              Nombre_Paciente = u.PNombre,
                              Dui_Paciente = u.DUI,
                              Fecha = c.Fecha,
                              Sintomas = c.Sntomas
                          });
            var finalcitas = citas.Concat(_citas);
            return finalcitas;
        }


        //GetCitasFilteredBySecretaria
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
                            nombre_Medico = m.MNombre,
                        });

            List<int> idMedicos = new List<int>();
            List<string> nMedicos = new List<string>();
            foreach (var item in info)
            {
                idMedicos.Add(item.id_Medico);
                nMedicos.Add(item.nombre_Medico);
            }
            var citas = context.Citas.Where(s => idMedicos.Contains(s.idMedico));

            int j = 0;
            foreach (var cita in citas)
            {
                for (int l = 0; l < idMedicos.Count; l++)
                {
                    if (cita.idMedico == idMedicos[l])
                    {
                        cita.Dui_D = nMedicos[l];
                    }
                }
            }
            return citas;
        }

        [HttpPost]
        public async Task<ActionResult<Citas>> Post([FromBody] Citas cita)
        {
            Console.WriteLine("Inicio" + cita);
            //var taskList = new List<Task>();
            if (cita.Dui_P == null || cita.Dui_D == null || cita.Dui_P == "" || cita.Dui_D == "") return Forbid();
            var doctor = context.Medico.FirstOrDefault(d => d.DUI == cita.Dui_D);
            var paciente = context.PacientesR.FirstOrDefault(p => p.DUI == cita.Dui_P);
            if (doctor == null) return NotFound();
            cita.idMedico = doctor.id_Medico;
            //Verificamos si está registrado
            if (paciente == null)
            {
                cita.Registered = false;
                //Puede no estar registrado pero existir en los datos de los no registrados
                var _paciente = context.PacientesNR.FirstOrDefault(p => p.DUI == cita.Dui_P);
                if (_paciente == null)
                {
                    //RegisterPacient
                    PacienteNR newPaciente = new PacienteNR();
                    newPaciente.DUI = cita.Dui_P;
                    context.PacientesNR.Add(newPaciente);
                    var success = await context.SaveChangesAsync();
                    if (success == 0)
                    {
                        return Forbid();
                    }
                    cita.id_Paciente = newPaciente.id;
                }
                else
                {
                    //PacienteExistsinNoRegistrados
                    cita.id_Paciente = _paciente.id;
                }
                    
            }
            else
            {
                //PacienteExists
                cita.id_Paciente = paciente.idPaciente;
                cita.Registered = true;
            }
            //WaitAllAwaitFunc
            //await Task.WhenAll(taskList);
            Console.WriteLine(cita);
            context.Citas.Add(cita);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
