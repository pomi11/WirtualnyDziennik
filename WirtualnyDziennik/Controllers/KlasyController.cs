using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class KlasyController : Controller
    {
        public ActionResult Index(int id, int przedmiotid)
        {           
            List<Klasy> klasy = new List<Klasy>();
            IList<Decimal> Lista;
            ViewData["przedmiotid"] = przedmiotid;
            using (ISession session = NhibernateSession.OpenSession())
            {
                var s = session.CreateSQLQuery("select k.id from planlekcji pl,przedmioty p,klasy k where pl.planlekcji_id=p.id and k.id=pl.klasa_id and p.id=" + id);

                Lista = s.List<Decimal>();
                for (int i = 0; i < Lista.Count(); i++)
                {
                    Klasy k = new Klasy();
                    
                    k = session.Get<Klasy>(Convert.ToInt32(Lista.ElementAt(i)));
                    klasy.Add(k);
            }
            }
            
            return View(klasy);
        }

        public ActionResult Details(int id)
        {
            Klasy klasy = new Klasy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                klasy = session.Query<Klasy>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(klasy);
        }

        public ActionResult Create()
        {
            /* Klasy klasy = new Klasy();
             using (ISession session = NhibernateSession.OpenSession())
             {

                 using (ITransaction transaction = session.BeginTransaction())   
                 {
                     session.Save(klasy);
                     transaction.Commit();  
                 }
             }*/
            return View();
        }

        [HttpPost]
        public ActionResult Create(String nazwa)
        {
            try
            {
                Klasy klasy = new Klasy();
                klasy.nazwa = nazwa;
                using (ISession session = NhibernateSession.OpenSession())
                {

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(klasy);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Klasy klasy = new Klasy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                klasy = session.Query<Klasy>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(klasy);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Klasy klasy = new Klasy();
                klasy.id = id;
                klasy.nazwa = collection["Nazwa"].ToString();
                using (ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(klasy);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Klasy klasy = new Klasy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                klasy = session.Query<Klasy>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", klasy);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Klasy klasy = session.Get<Klasy>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(klasy);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}