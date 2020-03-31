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
    public class HabitacionesController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Habitaciones
        public ActionResult Index(string searchString)
        {
            Tipo Doble;
            Tipo Privada;
            Tipo Suite;
            Doble = (Tipo)Enum.Parse(typeof(Tipo), "0");
            Privada = (Tipo)Enum.Parse(typeof(Tipo), "1");
            Suite = (Tipo)Enum.Parse(typeof(Tipo), "2");
            var habitaciones = from s in db.Habitaciones
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("Doble"))
                {
                    habitaciones = habitaciones.Where(s => s.Tipo == Doble);
                }
                else if (searchString.Contains("Privada"))
                {
                    habitaciones = habitaciones.Where(s => s.Tipo == Privada);
                }
                else if (searchString.Contains("Suite"))
                {
                    habitaciones = habitaciones.Where(s => s.Tipo == Suite);
                }
                
            }
            return View(habitaciones.ToList());
        }

        // GET: Habitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // GET: Habitaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Habitaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numero,Tipo,Precio_dia")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Habitaciones.Add(habitaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(habitaciones);
        }

        // GET: Habitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // POST: Habitaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero,Tipo,Precio_dia")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habitaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(habitaciones);
        }

        // GET: Habitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            db.Habitaciones.Remove(habitaciones);
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
    }
}
