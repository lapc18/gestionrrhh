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
    public class SalidasController : Controller
    {
        private GestionRRHHEntities db = new GestionRRHHEntities();

        // GET: Salidas
        public ActionResult Index()
        {
            var salidas = db.Salidas.Include(s => s.Empleado);
            return View(salidas.ToList());
        }

        public ActionResult Consulta()
        {
            var salidas = db.Salidas.Include(s => s.Empleado);
            return View(salidas.ToList());
        }

        [HttpPost]
        public ActionResult Consulta(string Mes)
        {
            var salidas = db.Salidas.Include(s => s.Empleado);

            if (string.IsNullOrEmpty(Mes))
            {
                return View(salidas.Where(x => x.FechaSalida.Month.ToString() == Mes).ToList());
            }
            else
            {
                return View(salidas.ToList());
            }
        }


        // GET: Salidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida salida = db.Salidas.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            return View(salida);
        }

        // GET: Salidas/Create
        public ActionResult Create()
        {
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre");
            return View();
        }

        // POST: Salidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodEmpleado,TipoSalida,Motivo,FechaSalida")] Salida salida)
        {
            if (ModelState.IsValid)
            {
                db.Salidas.Add(salida);
          
                //aqui inactivo el empleado
                db.Empleados.Find(salida.CodEmpleado).Estatus = "Inactivo";

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", salida.Empleado.Nombre);
            return View(salida);
        }

        // GET: Salidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida salida = db.Salidas.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", salida.Empleado.Nombre);
            return View(salida);
        }

        // POST: Salidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodEmpleado,TipoSalida,Motivo,FechaSalida")] Salida salida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "Id", "Nombre", salida.Empleado.Nombre);
            return View(salida);
        }

        // GET: Salidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida salida = db.Salidas.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            return View(salida);
        }

        // POST: Salidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salida salida = db.Salidas.Find(id);
            db.Salidas.Remove(salida);
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
