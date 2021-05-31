using ElBuenVivir.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElBuenVivir.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Citas> Citas { get; set; }
        public DbSet<HistorialMedico> Historial_Medico { get; set; }
        public DbSet<InfoAdmin> InfoAdmin { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<PacienteNR> PacientesNR { get; set; }
        public DbSet<Horarios> Horarios { get; set; }
        public DbSet<PacientesR> PacientesR { get; set; }
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<Secretaria> Secretarias { get; set; }

    }
}
