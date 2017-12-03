using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class TypUzytkownikaController : Controller
    {
        public ActionResult Index()
        {
            IList<TypUzytkownika> TUzytkownik;

            using (ISession session = NhibernateSession.OpenSession())
            {
                TUzytkownik = session.Query<TypUzytkownika>().ToList();
            }
            return View(TUzytkownik);
        }

        public ActionResult Details(int id)
        {
            TypUzytkownika TUzytkownik = new TypUzytkownika();
            using (ISession session = NhibernateSession.OpenSession())
            {
                TUzytkownik = session.Query<TypUzytkownika>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(TUzytkownik);
        }

        public ActionResult Create()
        {
            /* TypTresci TTresc = new TypTresci();
             using (ISession session = NhibernateSession.OpenSession())
             {

                 using (ITransaction transaction = session.BeginTransaction())   
                 {
                     session.Save(TTresc);
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
               TypUzytkownika TUzytkownik = new TypUzytkownika();
                TUzytkownik.nazwa = nazwa;
                using (ISession session = NhibernateSession.OpenSession())
                {

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(TUzytkownik);
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
            TypUzytkownika TUzytkownik = new TypUzytkownika();
            using (ISession session = NhibernateSession.OpenSession())
            {
                TUzytkownik = session.Query<TypUzytkownika>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(TUzytkownik);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                TypUzytkownika TUzytkownik = new TypUzytkownika();
                TUzytkownik.id = id;
                TUzytkownik.nazwa = collection["Nazwa"].ToString();
                using (ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(TUzytkownik);
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
            TypUzytkownika TUzytkownik = new TypUzytkownika();
            using (ISession session = NhibernateSession.OpenSession())
            {
                TUzytkownik = session.Query<TypUzytkownika>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", TUzytkownik);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    TypUzytkownika TUzytkownik = session.Get<TypUzytkownika>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(TUzytkownik);
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