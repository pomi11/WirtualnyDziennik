using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class KlasaUczenController : Controller
    {
        // GET: KlasaUczen
        public ActionResult Index()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //public ActionResult Delete(int id)
       // {
         //   return View();
        //}
        //[HttpPost]
        public ActionResult DeleteStudent(int id, FormCollection collection)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int id2;
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    //ISQLQuery s = session.CreateSQLQuery("select ku.klasa_id from klasauczen ku where ku.uzytkownik_id=" + id);
                    KlasaUczen ku = session.Query<KlasaUczen>().Where(b => b.Uzytkownik.id == id).First();
                    id2 = ku.Klasa.id;
                    KlasaUczen klasaUczen = session.Query<KlasaUczen>().Where(c => c.Uzytkownik.id == id).First();

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(klasaUczen);
                        trans.Commit();
                    }
                }
                return RedirectToAction("EditStudents","Klasy",new { id = id2 });
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}