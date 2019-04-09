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
    public class PermisosController : Controller
    {
        private GestionRRHHEntities db = new GestionRRHHEntities();

        // GET: Permisos
        public ActionResult Index()
        {
            var permisos = db.Permisos.Include(p => p.Empleado);
            return View(permisos.ToList());
        }




        [HttpPost]
        public ActionResult Consulta(string consulta)
        {
            var permisos = db.Permisos.Include(p => p.Empleado);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            
            if (consulta != null || !string.IsNullOrEmpty(consulta) || !string.IsNullOrWhiteSpace(consulta))
            {
                return View(permisos.Where(x => x.Empleado.Nombre == consulta).ToList());
            }
            else
            {
                return View(permisos.ToList());
            }

        }

        public ActionResult Consulta()
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            return View(empleados.ToList());
        }



        // GET: Permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permisos.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // GET: Permisos/Create
        public ActionResult Create()
        {
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre");
            return View();
        }

        // POST: Permisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodEmpleado,FechaInicio,FechaFin,Comentarios")] Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                db.Permisos.Add(permiso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", permiso.CodEmpleado);
            return View(permiso);
        }

        // GET: Permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permisos.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", permiso.CodEmpleado);
            return View(permiso);
        }

        // POST: Permisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodEmpleado,FechaInicio,FechaFin,Comentarios")] Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permiso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", permiso.CodEmpleado);
            return View(permiso);
        }

        // GET: Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permisos.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // POST: Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permiso permiso = db.Permisos.Find(id);
            db.Permisos.Remove(permiso);
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
