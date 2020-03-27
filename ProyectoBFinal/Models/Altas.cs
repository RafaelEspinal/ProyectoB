using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoBFinal.Models
{
    public class Altas
    {
        public int Id { get; set; }
        public string Nombre_paciente { get; set; }
        public string Fecha_ingreso { get; set; }
        public string Fecha_salida { get; set; }
        public string Habitacion { get; set; }
        public double Monto { get; set; }
        public int IngresoId { get; set; }
        [ForeignKey("IngresoId")]
        public Ingresos Ingresos { get; set; }
        public Pacientes Pacientes { get; set; }
        public Habitaciones Habitaciones { get; set; }


    }
}