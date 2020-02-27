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
    public class BaseController : Controller
    {
        protected FuhrparkmanagementModelContainer db = new FuhrparkmanagementModelContainer();
        private static String MitarbeiterIdKey = "MitarbeiterId";
        public Mitarbeiter setMitarbeiter()
        {
            int? MitarbeiterId = Session[MitarbeiterIdKey] as int?;
            if (MitarbeiterId == null) return null;
            Mitarbeiter res = db.Mitarbeiter.Find(MitarbeiterId);
            if (res == null) return null;
            ViewBag.Mitarbeiter = res;
            _LoggedInMitarbeiter = res;
            return res;
        }
        private Mitarbeiter _LoggedInMitarbeiter = null;
        protected Mitarbeiter LoggedInMitarbeiter => _LoggedInMitarbeiter ?? setMitarbeiter();

        protected bool hasMitarbeiter()
        {
            //hinterlegt, dass ein Mitarbeiter eingeloggt ist
            return LoggedInMitarbeiter != null;
        }
        protected void setMitarbeiterId(int MitarbeiterId)
        {
            //hinterlegt eingeloggten Mitarbeiter
            Session[MitarbeiterIdKey] = MitarbeiterId;
        }
        public bool getRoleAdmin()
        {
            if (LoggedInMitarbeiter != null)
            {
                //hinterlegt, dass ein Mitarbeiter Administrator ist
                //dem Administrator müssen verschiedene Sonderrechte und -funktionen ermöglicht werden
                return LoggedInMitarbeiter.IstAdmin;
            }
            return false;
        }
        public bool getRoleBuchender()
        {
            if (LoggedInMitarbeiter != null)
            {
                //hinterlegt, dass Mitarbeiter einen Füherschein besitzt
                //Fahrzeugbuchungen können nur von Mitarbeitern mit Führerschein getätigt werden
                return LoggedInMitarbeiter.Führerschein;
            }
            return false;
        }

    }
}
