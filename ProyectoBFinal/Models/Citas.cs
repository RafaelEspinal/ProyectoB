using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoBFinal.Models
{
    public class Citas
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

        public int Id_Paciente { get; set; }
        [ForeignKey("Id_Paciente")]
        public Pacientes Paciente { get; set; }

        public int Id_Medico { get; set; }
        [ForeignKey("Id_Medico")]
        public Medicos Medico { get; set; }
    }
}