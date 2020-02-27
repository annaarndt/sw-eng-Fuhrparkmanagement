using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fuhrparkmanagement;

namespace FuhrparkmanagementWebApp.Controllers
{
    public class FahrzeugsController : BaseController
    {
        
        // GET: Fahrzeugs
        // Fahrzeuge anzeigen
        public ActionResult Index()

        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            var fahrzeug = db.Fahrzeug.Include(f => f.IstVomTyp).Include(f => f.Hauptnutzer);
            return View(fahrzeug.ToList());
        }

        // GET: Fahrzeugs/Create
        // Fahrzeug hinzufügen
        public ActionResult Create()
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin(); //Anlage durch Administrator
            ViewBag.FahrzeugtypId = new SelectList(db.Fahrzeugtyp, "Id", "Modell");
            //Nacharbeit 0...1 Relation
            ViewBag.Hauptnutzer = new[] {new SelectListItem()
            {Text = "keiner", Value = "" } }.Union(
                new SelectList(db.Mitarbeiter, "Id", "Personalnummer"));
            return View();
        }

        // POST: Fahrzeugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Fahrzeug hinzufügen - speichern
        public ActionResult Create([Bind(Include = "Id,Kennzeichen,Farbe,Kilometerstand,FahrzeugtypId,Hauptnutzer")] Fahrzeug fahrzeug)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (ModelState.IsValid)
            {   try
                {
                  db.Fahrzeug.Add(fahrzeug);
                  db.SaveChanges();
                }
                catch (DbEntityValidationException e) //Ausgabe Fehlerinformationen bei Fehlermeldungen
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
                ViewBag.FahrzeugtypId = new SelectList(db.Fahrzeugtyp, "Id", "Modell", fahrzeug.FahrzeugtypId);
                ViewBag.Hauptnutzer = new SelectList(db.Mitarbeiter, "Id", "Personalnummer", fahrzeug.Hauptnutzer);
                return View(fahrzeug);        
        }
        // GET: Fahrzeugs/Edit/5
        // Fahrzeug bearbeiten
        public ActionResult Edit(int? id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fahrzeug fahrzeug = db.Fahrzeug.Find(id);
            if (fahrzeug == null)
            {
                return HttpNotFound();
            }
            ViewBag.FahrzeugtypId = new SelectList(db.Fahrzeugtyp, "Id", "Modell", fahrzeug.FahrzeugtypId);
            //Nacharbeit 0...1 Relation
            ViewBag.Hauptnutzer = new[] {new SelectListItem()
            {Text = "keiner", Value = "" } }.Union(
                new SelectList(db.Mitarbeiter, "Id", "Personalnummer"));
            return View(fahrzeug);
        }

        // POST: Fahrzeugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Fahrzeug bearbeiten - speichern
        public ActionResult Edit([Bind(Include = "Id,Kennzeichen,Farbe,Kilometerstand,FahrzeugtypId,Hauptnutzer")] Fahrzeug fahrzeug)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (ModelState.IsValid)
            {
                db.Entry(fahrzeug).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FahrzeugtypId = new SelectList(db.Fahrzeugtyp, "Id", "Modell", fahrzeug.FahrzeugtypId);
            ViewBag.Hauptnutzer = new SelectList(db.Mitarbeiter, "Id", "Personalnummer", fahrzeug.Hauptnutzer);
            return View(fahrzeug);
        }

        // GET: Fahrzeugs/Delete/5
        // Fahrzeug löschen
        public ActionResult Delete(int? id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fahrzeug fahrzeug = db.Fahrzeug.Find(id);
            if (fahrzeug == null)
            {
                return HttpNotFound();
            }
            return View(fahrzeug);
        }

        // POST: Fahrzeugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Fahrzeug löschen - speichern
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Admin = getRoleAdmin();
            Fahrzeug fahrzeug = db.Fahrzeug.Find(id);
            db.Fahrzeug.Remove(fahrzeug);
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
