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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Klasy> kl;

            using (ISession session = NhibernateSession.OpenSession())
            {
                kl = session.Query<Klasy>().Where(b => b.Wychowawca.id == id).ToList();
            }
            return View(kl);
        }

        public ActionResult ClassesStudentList(int id,int planlekcjiid)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserProfileSessionData upsd = Session["UserProfile"] as UserProfileSessionData;
            IList<Object[]> lista;
            IQuery s = null;
            using (ISession session = NhibernateSession.OpenSession())
            {

                s = session.CreateSQLQuery("select u.id, u.imie, u.nazwisko from uzytkownicy u, klasauczen ku, klasy k where u.id = ku.uzytkownik_id and ku.klasa_id = k.id and k.id = " + id);
                //U = session.Query<Uzytkownicy>().Where(c => c.id == session.Query<KlasaUczen>().Where(b => b.Klasy.id == session.Query<Klasy>().Where(a => a.Wychowawca.id == id).First().id).First().klasauczen_id).ToList();
                lista = s.List<Object[]>();
            }
            ViewData["planlekcjiid"] = planlekcjiid;
            return View(lista);
        }
        public ActionResult ListaLekcji(int id,int przedmiotid)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<PlanLekcji> lista;
            using (ISession session = NhibernateSession.OpenSession())
            {

                lista = session.Query<PlanLekcji>().Where(b => b.Klasa.id == id).Where(b=>b.Przedmiot.id==przedmiotid).ToList();
            }
            ViewData["przedmiotid"] = przedmiotid;
            ViewData["klasaid"] = id;
            return View(lista);
        }
   
    }
}