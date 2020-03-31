using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoBFinal.Models;

namespace ProyectoBFinal.Controllers
{
    public class AltasController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Altas
        public ActionResult Index(string searchString)
        {
            var altas = db.Altas.Include(a => a.Ingresos).Include(a => a.Pacientes).Include(a => a.Habitaciones);
            altas = from s in altas
                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {

                altas = altas.Where(s => s.Pacientes.Nombre.Contains(searchString)
                                        || s.Fecha_ingreso.Contains(searchString)
                                        || s.Fecha_salida.Contains(searchString));

            }
            return View(altas.ToList());
        }

        // GET: Altas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // GET: Altas/Create
        public ActionResult Create()
        {
            ViewBag.IngresoId = new SelectList(db.Ingresos, "Id", "Id");
            return View();
        }

        // POST: Altas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre_paciente,Fecha_ingreso,Fecha_salida,Habitacion,Monto,IngresoId")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Altas.Add(altas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngresoId = new SelectList(db.Ingresos, "Id", "Id", altas.IngresoId);
            return View(altas);
        }

        // GET: Altas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngresoId = new SelectList(db.Ingresos, "Id", "Fecha_ingreso", altas.IngresoId);
            return View(altas);
        }

        // POST: Altas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre_paciente,Fecha_ingreso,Fecha_salida,Habitacion,Monto,IngresoId")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngresoId = new SelectList(db.Ingresos, "Id", "Fecha_ingreso", altas.IngresoId);
            return View(altas);
        }

        // GET: Altas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // POST: Altas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Altas altas = db.Altas.Find(id);
            db.Altas.Remove(altas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
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
