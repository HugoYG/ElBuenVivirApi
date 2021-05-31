using ElBuenVivir.Context;
using ElBuenVivir.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElBuenVivir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly AppDbContext context;

        public HorariosController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Horarios Get(int id)
        {
            return context.Horarios.FirstOrDefault(p => p.id == id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
