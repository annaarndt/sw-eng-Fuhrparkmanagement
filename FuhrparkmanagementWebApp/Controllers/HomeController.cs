using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuhrparkmanagementWebApp.Controllers
{
    public class HomeController : BaseController
    {   // Startseite
        public ActionResult Index()
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            return View();
        }
        // Kontakt
        public ActionResult Contact()
        {
            ViewBag.Buchender = getRoleBuchender();
            ViewBag.Admin = getRoleAdmin();
            ViewBag.Login = hasMitarbeiter();
            ViewBag.Message = "Ihre Kontaktmöglichkeiten";

            return View();
        }
    }
}