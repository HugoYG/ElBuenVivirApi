using ElBuenVivir.Context;
using ElBuenVivir.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretariasController : ControllerBase
    {
        private readonly AppDbContext context;

        public SecretariasController(AppDbContext context)
        {
            this.context = context;
        }

        //GetAllSecretarias
        [HttpGet]
        public IEnumerable Get()
        {
            var secretarias = (from u in context.Secretarias
                               select new Secretaria{
                                   id_Secretaria = u.id_Secretaria,
                                   S_Nombre = u.S_Nombre,
                                   S_Password = u.S_Password,
                                   S_Email = u.S_Email
                               });
            return secretarias;
        }

        [HttpGet("{id}")]
        public IQueryable Get(string id)
        {
            int _id = int.Parse(id);
            var secretaria = (from s in context.Secretarias
                              where s.id_Secretaria == _id
                              select new Secretaria
                              {
                                  id_Secretaria = s.id_Secretaria,
                                  S_Nombre = s.S_Nombre,
                                  S_Password = s.S_Password,
                                  S_Email = s.S_Email
                              });
            return secretaria;
        }

        [HttpPost]
        public async Task<ActionResult<Secretaria>> Post([FromBody] Secretaria secret)
        {
            if (secret.S_Email == null || secret.S_Password == null) return BadRequest(new { message = "Username or password empty" });
            else
            {
                var _secretaria = context.Secretarias.FirstOrDefault(s => s.S_Email == secret.S_Email && s.S_Password == secret.S_Password);
                if(_secretaria != null)
                {
                    return _secretaria;
                }
                else 
                {
                    return NotFound();
                }
            }
        }

    }
}
