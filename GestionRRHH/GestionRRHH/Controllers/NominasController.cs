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
    public class NominasController : Controller
    {
        private GestionRRHHEntities db = new GestionRRHHEntities();

        // GET: Nominas
        public ActionResult Index()
        {
            ViewBag.Monto = db.Empleados.Sum(x => x.Salario);
            return View(db.Nominas.ToList());
        }

        public ActionResult Consulta()
        {
            return View(db.Nominas.ToList());
        }

        [HttpPost]
        public ActionResult Consulta(string Mes, int year)
        {
            if (year.ToString() != null)
            {
              return View(db.Nominas.Where(x => x.C_Year == year && x.Mes.ToString().Contains(Mes)).ToList());
            }
            else
            {
                return View(db.Nominas.ToList());
            }
            
        }

        public ActionResult Calculo()
        {
            ViewBag.Year = DateTime.Now.Year;
            ViewBag.Mes = DateTime.Now.Month;
            ViewBag.Monto = db.Empleados.Sum(x => x.Salario);
            var nomina = new Nomina {
                C_Year = DateTime.Now.Year,
                Mes = DateTime.Now.Month,
                MontoTotal = int.Parse(db.Empleados.Sum(x => x.Salario).ToString())
            };

            db.Nominas.Add(nomina);
            db.SaveChanges();

            return View(nomina);
        }

        // GET: Nominas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // GET: Nominas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nominas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,C_Year,Mes,MontoTotal")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Nominas.Add(nomina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nomina);
        }

        // GET: Nominas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nominas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,C_Year,Mes,MontoTotal")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nomina);
        }

        // GET: Nominas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nomina nomina = db.Nominas.Find(id);
            db.Nominas.Remove(nomina);
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
