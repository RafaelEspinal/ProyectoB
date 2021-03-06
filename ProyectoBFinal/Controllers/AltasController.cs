﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoBFinal.Models;
using Rotativa;

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
            else
            {
                ViewBag.Sumatoria = altas.Sum(s => s.Monto);
                ViewBag.Conteo = altas.Count();
                ViewBag.Promedio = altas.Average(s => s.Monto);
                ViewBag.Max = altas.Max(s => s.Monto);
                ViewBag.Min = altas.Min(s => s.Monto);
            }
            

            return View(altas.ToList());
        }

        public ActionResult Imprimir()
        {
            var imprimir = new ActionAsPdf("Index");
            return imprimir;
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
        
    }
}
