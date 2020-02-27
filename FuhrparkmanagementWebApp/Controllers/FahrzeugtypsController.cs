using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fuhrparkmanagement;

namespace FuhrparkmanagementWebApp.Controllers
{
    public class FahrzeugtypsController : BaseController
    {
        
        // GET: Fahrzeugtyps
        // Fahrzeugtypen anzeigen
        public ActionResult Index()
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            return View(db.Fahrzeugtyp.ToList());
        }

        // GET: Fahrzeugtyps/Create
        // Fahrzeugtyp anlegen
        public ActionResult Create()
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            return View();
        }

        // POST: Fahrzeugtyps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Fahrzeugtyp anlegen - speichern
        public ActionResult Create([Bind(Include = "Id,Modell,Leistung,Sitzplatzanzahl")] Fahrzeugtyp fahrzeugtyp)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (ModelState.IsValid)
            {
                db.Fahrzeugtyp.Add(fahrzeugtyp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fahrzeugtyp);
        }

        // GET: Fahrzeugtyps/Edit/5
        // Fahrzeugtyp bearbeiten
        public ActionResult Edit(int? id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fahrzeugtyp fahrzeugtyp = db.Fahrzeugtyp.Find(id);
            if (fahrzeugtyp == null)
            {
                return HttpNotFound();
            }
            return View(fahrzeugtyp);
        }

        // POST: Fahrzeugtyps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Fahrzeugtyp bearbeiten - speichern
        public ActionResult Edit([Bind(Include = "Id,Modell,Leistung,Sitzplatzanzahl")] Fahrzeugtyp fahrzeugtyp)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (ModelState.IsValid)
            {
                db.Entry(fahrzeugtyp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fahrzeugtyp);
        }

        // GET: Fahrzeugtyps/Delete/5
        // Fahrzeugtyp löschen
        public ActionResult Delete(int? id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fahrzeugtyp fahrzeugtyp = db.Fahrzeugtyp.Find(id);
            if (fahrzeugtyp == null)
            {
                return HttpNotFound();
            }
            return View(fahrzeugtyp);
        }

        // POST: Fahrzeugtyps/Delete/5
        // Fahrzeugtyp löschen - speichern
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            Fahrzeugtyp fahrzeugtyp = db.Fahrzeugtyp.Find(id);
            db.Fahrzeugtyp.Remove(fahrzeugtyp);
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
