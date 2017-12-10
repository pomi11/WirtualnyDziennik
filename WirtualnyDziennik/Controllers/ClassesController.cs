using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ActionResult Index(int id)
        {
            List<Klasy> kl;

            using (ISession session = NhibernateSession.OpenSession())
            {
                kl = session.Query<Klasy>().Where(b => b.Wychowawca.id == id).ToList();
            }
            return View(kl);
        }

        public ActionResult ClassesStudentList(int id)
        {
            UserProfileSessionData upsd = Session["UserProfile"] as UserProfileSessionData;
            IList<Object[]> lista;
            IQuery s = null;
            using (ISession session = NhibernateSession.OpenSession())
            {

                s = session.CreateSQLQuery("select u.imie, u.nazwisko from uzytkownicy u, klasauczen ku, klasy k where u.id = ku.uzytkownik_id and ku.klasa_id = k.id and k.id = " + id);
                lista = s.List<Object[]>();
            }
            return View(lista);
        }
    }
}