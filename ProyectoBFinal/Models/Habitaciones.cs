using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoBFinal.Models
{
    public class Habitaciones
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public Tipo Tipo { get; set; }
        public double Precio_dia { get; set; }


    }
    public enum Tipo
    {
        Doble, Privada, Suite
    }
}