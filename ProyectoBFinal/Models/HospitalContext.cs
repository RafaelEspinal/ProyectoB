using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoBFinal.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        : base("cadena1")
        { }

        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<Habitaciones> Habitaciones { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<Altas> Altas  { get; set; }

        public DbSet<Ingresos> Ingresos { get; set; }

    }
}