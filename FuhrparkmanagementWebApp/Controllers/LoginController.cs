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
    public class LoginController : BaseController
    {
        // GET: Login
        // Mitarbeiter einloggen - Übersicht
        public ActionResult Index()
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            return View(db.Mitarbeiter.ToList());
        }
        // Mitarbeiter einloggen - Auswahl
        public ActionResult Select(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }

            setMitarbeiterId(id.Value);
            return RedirectToAction("Index");
        }
    }
}
