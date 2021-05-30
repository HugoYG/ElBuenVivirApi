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
                               select new{
                                   id = u.id_Secretaria,
                                   Nombre = u.S_Nombre,
                                   Password = u.S_Password,
                                   Email = u.S_Email
                               });
            return secretarias;
        }

        [HttpGet("{id}")]
        public IQueryable Get(string id)
        {
            int _id = int.Parse(id);
            var secretaria = (from s in context.Secretarias
                              where s.id_Secretaria == _id
                              select new
                              {
                                  id = s.id_Secretaria,
                                  Nombre = s.S_Nombre,
                                  Password = s.S_Password,
                                  Email = s.S_Email
                              });
            return secretaria;
        }

        [HttpPost]
        public async Task<ActionResult<Secretaria>> Post([FromBody] Secretaria secret)
        {
            if (secret.S_Email == null || secret.S_Password == null) return BadRequest(new { message = "Username or password empty" });
            else
            {
                var _secretaria = context.Secretarias.FirstOrDefault(s => s.S_Email == secret.S_Email);
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
