using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionRRHH.Models;

namespace GestionRRHH.Controllers
{
    public class VacacionesController : Controller
    {
        private GestionRRHHEntities db = new GestionRRHHEntities();

        // GET: Vacaciones
        public ActionResult Index()
        {
            var vacaciones = db.Vacaciones.Include(v => v.Empleado);
            return View(vacaciones.ToList());
        }

        // GET: Vacaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacacione vacacione = db.Vacaciones.Find(id);
            if (vacacione == null)
            {
                return HttpNotFound();
            }
            return View(vacacione);
        }

        // GET: Vacaciones/Create
        public ActionResult Create()
        {
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre");
            return View();
        }

        // POST: Vacaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodEmpleado,FechaInicio,FechaFin,Correspondiente,Comentario")] Vacacione vacacione)
        {
            if (ModelState.IsValid)
            {
                db.Vacaciones.Add(vacacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre");
            return View(vacacione);
        }

        // GET: Vacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacacione vacacione = db.Vacaciones.Find(id);
            if (vacacione == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", vacacione.CodEmpleado);
            return View(vacacione);
        }

        // POST: Vacaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodEmpleado,FechaInicio,FechaFin,Correspondiente,Comentario")] Vacacione vacacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", vacacione.CodEmpleado);
            return View(vacacione);
        }

        // GET: Vacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacacione vacacione = db.Vacaciones.Find(id);
            if (vacacione == null)
            {
                return HttpNotFound();
            }
            return View(vacacione);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacacione vacacione = db.Vacaciones.Find(id);
            db.Vacaciones.Remove(vacacione);
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
