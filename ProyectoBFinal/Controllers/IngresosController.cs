﻿using System;
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
    public class IngresosController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Ingresos
        public ActionResult Index(string searchString)
        {
            Tipo Doble;
            Tipo Privada;
            Tipo Suite;
            Doble = (Tipo)Enum.Parse(typeof(Tipo), "0");
            Privada = (Tipo)Enum.Parse(typeof(Tipo), "1");
            Suite = (Tipo)Enum.Parse(typeof(Tipo), "2");

            var ingresos = db.Ingresos.Include(i => i.Habitacion).Include(i => i.Paciente);

            ingresos = from s in ingresos
                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("Doble"))
                {
                    ingresos = ingresos.Where(s => s.Fecha_ingreso.Contains(searchString) 
                                                || s.Habitacion.Tipo == Doble);
                }
                else if (searchString.Contains("Privada"))
                {
                    ingresos = ingresos.Where(s => s.Fecha_ingreso.Contains(searchString)
                                                || s.Habitacion.Tipo == Privada);
                }
                else if (searchString.Contains("Suite"))
                {
                    ingresos = ingresos.Where(s => s.Fecha_ingreso.Contains(searchString)
                                               || s.Habitacion.Tipo == Suite);
                }
                else
                {
                    ingresos = ingresos.Where(s => s.Fecha_ingreso.Contains(searchString));
                }

            }
            return View(ingresos.ToList());
        }

        // GET: Ingresos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // GET: Ingresos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Habitacion = new SelectList(db.Habitaciones, "Id", "Tipo");
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id", "Nombre");
            return View();
        }

        // POST: Ingresos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha_ingreso,Id_Paciente,Id_Habitacion")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Ingresos.Add(ingresos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Habitacion = new SelectList(db.Habitaciones, "Id", "Tipo", ingresos.Id_Habitacion);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id", "Nombre", ingresos.Id_Paciente);
            return View(ingresos);
        }

        // GET: Ingresos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Habitacion = new SelectList(db.Habitaciones, "Id", "Tipo", ingresos.Id_Habitacion);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id", "Nombre", ingresos.Id_Paciente);
            return View(ingresos);
        }

        // POST: Ingresos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha_ingreso,Id_Paciente,Id_Habitacion")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingresos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Habitacion = new SelectList(db.Habitaciones, "Id", "Tipo", ingresos.Id_Habitacion);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id", "Nombre", ingresos.Id_Paciente);
            return View(ingresos);
        }

        // GET: Ingresos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingresos ingresos = db.Ingresos.Find(id);
            db.Ingresos.Remove(ingresos);
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
