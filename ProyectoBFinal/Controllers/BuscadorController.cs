using ProyectoBFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoBFinal.Controllers
{
    public class BuscadorController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Buscador
        [HttpPost]
        public JsonResult BuscadorNom(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join p in db.Pacientes
                             on i.Id_Paciente equals p.Id
                             where i.Id == clavePaciente
                             select p.Nombre).ToList();
            return Json(duplicado);
        }

        public JsonResult BuscadorMonto(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.Id_Habitacion equals h.Id
                             where i.Id == clavePaciente
                             select h.Precio_dia).ToList();
            return Json(duplicado);
        }
        public JsonResult BuscadorFIn(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             where i.Id == clavePaciente
                             select i.Fecha_ingreso).ToList();
            return Json(duplicado);
        }
        public JsonResult BuscadorNHab(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.Id_Habitacion equals h.Id
                             where i.Id == clavePaciente
                             select h.Numero).ToList();
            return Json(duplicado);
        }
        
    }
}