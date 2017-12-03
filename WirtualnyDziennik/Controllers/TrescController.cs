using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class TrescController : Controller
    {
        public ActionResult Index()
        {
            List<Tresc> tresc;

            using (ISession session = NhibernateSession.OpenSession())
            {
                tresc = session.Query<Tresc>().ToList();
            }
            
            return View(tresc);
        }

        public ActionResult Details(int id)
        {
            Tresc tresc = new Tresc();
            using (ISession session = NhibernateSession.OpenSession())
            {
                tresc = session.Query<Tresc>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(tresc);
        }

        public ActionResult Create()
        {
            //Tresc tr = new Tresc();
            //tr.tresc = "asda";
            //tr.tytul = "pierwsze";
           // tr.typtresci_id = 2;
            using (ISession session = NhibernateSession.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                   // session.Save(/*tr*/);
                    transaction.Commit();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(String nazwa)
        {
            try
            {
                Tresc tresc = new Tresc();
                tresc.tresc = nazwa;
                using (ISession session = NhibernateSession.OpenSession())
                {

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(tresc);
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
            Tresc tresc = new Tresc();
            using (ISession session = NhibernateSession.OpenSession())
            {
                tresc = session.Query<Tresc>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(tresc);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Tresc tresc = new Tresc();
                tresc.id = id;
                tresc.tresc = collection["Nazwa"].ToString();
                using (ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(tresc);
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
            Tresc tresc = new Tresc();
            using (ISession session = NhibernateSession.OpenSession())
            {
                tresc = session.Query<Tresc>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", tresc);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Tresc tresc = session.Get<Tresc>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(tresc);
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