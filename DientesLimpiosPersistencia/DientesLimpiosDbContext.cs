using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public class DientesLimpiosDbContext : DbContext
    {
        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options) : base(options)
        {
        }

        protected DientesLimpiosDbContext()
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDbContext).Assembly);
        }


        public DbSet<Consultorio>   Consultorios { get; set; }

    }
}
