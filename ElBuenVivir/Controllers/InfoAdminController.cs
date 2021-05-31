using ElBuenVivir.Context;
using ElBuenVivir.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElBuenVivir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public InfoAdminController(AppDbContext context)
        {
            this.context = context;
        }

        //GetAll
        [HttpGet]
        public IQueryable Get()
        {
            var info = (from i in context.InfoAdmin 
                        join m in context.Secretarias on i.id_Secretaria equals m.id_Secretaria
                        join d in context.Medico on i.id_Medico equals d.id_Medico
                        select new
                        {
                            id = i.id_Info,
                            id_Secretaria = i.id_Secretaria,
                            Nombre_Secretaria = m.S_Nombre,
                            id_Medico = i.id_Medico,
                            Nombre_Medico = d.MNombre
                        });
            return info;
        }

        [HttpGet("secretaria/{id}")]
        public IQueryable GetById(string id)
        {
            int _id;
            bool success = int.TryParse(id, out _id);
            if(success)
            {
                var info = (from i in context.InfoAdmin
                            join s in context.Secretarias on i.id_Secretaria equals s.id_Secretaria
                            join m in context.Medico on i.id_Medico equals m.id_Medico
                            where s.id_Secretaria == _id
                            select new
                            {
                                id = i.id_Info,
                                id_Secretaria = i.id_Secretaria,
                                Nombre_Secretaria = s.S_Nombre,
                                id_Medico = i.id_Medico,
                                Nombre_Medico = m.MNombre,
                                Especialidad = m.Area_Especialidad,
                                Dui_Medico = m.DUI,
                                Dias_laborales = m.Dias_Laborales,
                                Horarios = m.Horarios_Consulta
                            });
                return info;
            }
            else
            {
                return null;
            }
            
        }


        [HttpPost]
        public async Task<ActionResult<InfoAdmin>> Post([FromBody] InfoAdmin info)
        {
            context.InfoAdmin.Add(info);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), info);
        }

    }
}
