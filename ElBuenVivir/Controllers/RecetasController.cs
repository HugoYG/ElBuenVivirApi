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
    public class RecetasController : ControllerBase
    {
        private readonly AppDbContext context;

        public RecetasController(AppDbContext context)
        {
            this.context = context;
        }

        //GetAll
        [HttpGet]
        public IQueryable Get()
        {
            var info = (from r in context.Recetas
                        select new 
                        {
                            id = r.idreceta,
                            precio = r.Precio,
                            Fecha = r.Fecha
                        });
            return info;
        }

        [HttpPost]
        public async Task<ActionResult<Recetas>> Post([FromBody] Recetas receta)
        {
            context.Recetas.Add(receta);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), receta);
        }

    }
}
