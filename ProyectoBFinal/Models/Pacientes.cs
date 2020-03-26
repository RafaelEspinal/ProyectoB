using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoBFinal.Models
{
    public class Pacientes
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public Asegurados Asegurado { get; set; }
    }

    public enum Asegurados
    {
        Si, No
    }
}