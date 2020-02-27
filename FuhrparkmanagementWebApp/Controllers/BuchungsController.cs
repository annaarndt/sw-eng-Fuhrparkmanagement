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
    public class BuchungsController : BaseController
    {
        
        // GET: Buchungs
        // Buchungen anzeigen
        public ActionResult Index() 
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Mitarbeiter = setMitarbeiter();
            
            var buchung = db.Buchung.Include(b => b.AngelegtVon).Include(b => b.MitFahrzeug);

            return View(buchung.ToList());
        }

        // GET: Buchungs/Create
        // Buchung anlegen
        public ActionResult Create()
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            ViewBag.FahrzeugId = new[] {new SelectListItem()
            {Text = "keiner", Value = "" } }.Union(
                new SelectList(db.Fahrzeug, "Id", "Kennzeichen"));
            //Nacharbeit Multiplizität 0...1

            return View();
        }

        // POST: Buchungs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Buchung anlegen - speichern
        public ActionResult Create([Bind(Include = "Id,Zweck,Start,Ende,Kilometerplan,MitarbeiterId")] Buchung buchung)
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            buchung.AngelegtVon = LoggedInMitarbeiter;
                        
            if (ModelState.IsValid)
            {
                try 
                {                 
                    {
                        db.Buchung.Add(buchung);
                        db.SaveChanges();
                    }
                }
                catch (DbEntityValidationException e) //Überprüfung in welcher Zeile eine Fehlermeldung entsteht
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
            
            return View(buchung);
        }
       
        // GET: Buchungs/Edit/5
        // Buchung bearbeiten bzw. Fahrzeug hinzufügen - Verfügbarkeitsprüfung
        public ActionResult Edit(int? id)
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buchung buchung = db.Buchung.Find(id);
            if (buchung == null)
            {
                return HttpNotFound();
            }
            
            List<Fahrzeug> freiefahrzeuge = new List <Fahrzeug>(db.Fahrzeug); //listet alle Fahrzeuge in einer Liste "freiefahrzeuge" auf

            //Überprüfung Verfügbarkeit der Fahrzeuge:
            //selektiert Buchungen, die ein Fahrzeug beinhalten in einer IQueryable
            var bmitfahrzeug = (from Buchung in db.Buchung
                                     where Buchung.FahrzeugId != null
                                     select Buchung);    
            //Umwandlung der IQueryable in eine Liste mit den relevanten Attriubuten Id, Start, Ende und FahrzeugId
            var bmitfahrzeuglist = bmitfahrzeug.Select(b => new {b.Id, b.Start, b.Ende, b.FahrzeugId, b.MitFahrzeug}).ToList(); 

            //bmitfahrzeuglist beinhaltet eine Liste aller Buchungen, die Fahrzeuge enthalten
            foreach (var b in bmitfahrzeuglist)
             {
                int result1 = DateTime.Compare(b.Start, buchung.Start);
                int result2 = DateTime.Compare(b.Ende, buchung.Start);
                int result3 = DateTime.Compare(buchung.Ende, b.Start);
                int result4 = DateTime.Compare(buchung.Ende, b.Ende);
                //Überprüfung, ob bisherige Buchungen sich mit der neuen Buchung überschneiden

                if //falls sich die Buchungen nicht überschneiden, sind die dort enthaltenen Fahrzeuge verfügbar
                ((result1 < 0 & result2 < 0 )| (result3 < 0 & result4 < 0)) 
                { }
                else //ansonsten müssen die zu dieser Zeit belegten Fahrzeuge von der Liste entfernt werden
                { freiefahrzeuge.Remove(b.MitFahrzeug); } 
            }
            
            //Rückgabe der Liste "freiefahrzeuge" an ViewBag zur Auswahl für den Mitarbeiter
            ViewBag.FahrzeugId = new[] {new SelectListItem()
             { Text = "kein Fahrzeug", Value = "" } }.Union(
             new SelectList(freiefahrzeuge, "Id", "Kennzeichen"));
        
            return View(buchung);
        }

        // POST: Buchungs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Buchung bearbeiten bzw. Fahrzeug hinzufügen - Speichern
        public ActionResult Edit([Bind(Include = "Id,Zweck,Start,Ende,Kilometerplan,MitarbeiterId,FahrzeugId")] Buchung buchung)
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            if (ModelState.IsValid)
            {
                try
                {   //nur die Information, welcher Mitarbeiter die Buchung angelegt hat, soll nicht geändert werden können
                    var excluded = new[] { "MitarbeiterId" };
                    var entry = db.Entry(buchung);
                    entry.State = EntityState.Modified;
                    foreach (var m in excluded)
                    { entry.Property(m).IsModified = false; }
                    
                    db.SaveChanges();
                    
                }
                catch (DbEntityValidationException e) //Überprüfung in welcher Zeile eine Fehlermeldung entsteht
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
            ViewBag.FahrzeugId = new SelectList(db.Fahrzeug, "Id", "Kennzeichen", buchung.FahrzeugId);
            return View(buchung);
        }

        // GET: Buchungs/Delete/5
        // Buchung löschen
        public ActionResult Delete(int? id)
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buchung buchung = db.Buchung.Find(id);
            if (buchung == null)
            {
                return HttpNotFound();
            }
            return View(buchung);
        }

        // POST: Buchungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Buchung löschen - speichern
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            Buchung buchung = db.Buchung.Find(id);
            db.Buchung.Remove(buchung);
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
