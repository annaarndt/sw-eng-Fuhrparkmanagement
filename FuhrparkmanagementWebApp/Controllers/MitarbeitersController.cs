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
    public class MitarbeitersController : BaseController
    {
        
        // GET: Mitarbeiters
        // Mitarbeiter anzeigen
        public ActionResult Index()
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            return View(db.Mitarbeiter.ToList());
        }

        // GET: Mitarbeiters/Create
        // Mitarbeiter hinzufügen
        public ActionResult Create()
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            return View();
        }

        // POST: Mitarbeiters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Mitarbeiter hinzufügen - speichern
        public ActionResult Create([Bind(Include = "Id,Personalnummer,Name,Vorname,Führerschein,Führerscheinnummer,IstAdmin")] Mitarbeiter mitarbeiter)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (ModelState.IsValid)
            {
                db.Mitarbeiter.Add(mitarbeiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Edit/5
        // Mitarbeiter bearbeiten
        public ActionResult Edit(int? id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Mitarbeiter bearbeiten - speichern
        public ActionResult Edit([Bind(Include = "Id,Personalnummer,Name,Vorname,Führerschein,Führerscheinnummer, IstAdmin")] Mitarbeiter mitarbeiter)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (ModelState.IsValid)
            {
                db.Entry(mitarbeiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Delete/5
        // Mitarbeiter löschen
        public ActionResult Delete(int? id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Mitarbeiter löschen - speichern
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            db.Mitarbeiter.Remove(mitarbeiter);
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
