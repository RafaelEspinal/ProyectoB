using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoBFinal.Models
{
    public class Ingresos
    {
        public int Id { get; set; }
        public string Fecha_ingreso { get; set; }

        public int Id_Paciente { get; set; }
        [ForeignKey("Id_Paciente")]
        public Pacientes Paciente { get; set; }

        public int Id_Habitacion { get; set; }
        [ForeignKey("Id_Habitacion")]
        public Habitaciones Habitacion { get; set; }
    }
}