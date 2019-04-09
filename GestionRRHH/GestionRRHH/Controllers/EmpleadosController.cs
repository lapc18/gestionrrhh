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
    public class EmpleadosController : Controller
    {
        private GestionRRHHEntities db = new GestionRRHHEntities();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            return View(empleados.Where(x => x.Estatus == "Activo").ToList());
        }


        [HttpPost]
        public ActionResult Inactivo(int Departamento, string consulta)
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            if (consulta != null || !string.IsNullOrEmpty(consulta) || !string.IsNullOrWhiteSpace(""))
            {
                return View(empleados.Where(x => x.Estatus == "Inactivo" && x.Nombre.Contains(consulta) && x.Departamento == Departamento).ToList());
            }
            else
            {
                return View(empleados.ToList());
            }

        }

        public ActionResult Inactivo()
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            return View(empleados.ToList());
        }



        [HttpPost]
        public ActionResult Consulta(int Departamento, string consulta)
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            if (consulta != null || !string.IsNullOrEmpty(consulta) || !string.IsNullOrWhiteSpace(""))
            {
                return View(empleados.Where(x => x.Estatus == "Activo" && x.Nombre.Contains(consulta) && x.Departamento == Departamento).ToList());
            }
            else{
                return View(empleados.ToList()); 
            }
           
        }

        public ActionResult Consulta()
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            return View(empleados.ToList());
        }


        [HttpPost]
        public ActionResult ConsultaFull(string Mes)
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            
            if (Mes != null || !string.IsNullOrEmpty(Mes) || !string.IsNullOrWhiteSpace(Mes))
            {
                return View(empleados.Where(x => x.FechaIngreso.Value.Month.ToString() == Mes).ToList());
            }
            else
            {
                return View(empleados.ToList());
            }

        }

        public ActionResult ConsultaFull()
        {
            var empleados = db.Empleados.Include(e => e.Cargo).Include(e => e.Departamento1);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.CodCargo = new SelectList(db.Cargos, "Id", "Cargos");
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Nombre,Apellido,Telefono,Departamento,CodCargo,FechaIngreso,Salario,Estatus")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodCargo = new SelectList(db.Cargos, "Id", "Cargos", empleado.CodCargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Codigo", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodCargo = new SelectList(db.Cargos, "Id", "Cargos", empleado.CodCargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre", empleado.Departamento);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Nombre,Apellido,Telefono,Departamento,CodCargo,FechaIngreso,Salario,Estatus")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodCargo = new SelectList(db.Cargos, "Id", "Cargos", empleado.CodCargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            empleado.Estatus = "Inactivo";

           
            //db.Empleados.Remove(empleado);
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
