using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class TypTresciController : Controller
    {
         public ActionResult Index()
        {
            IList<TypTresci> TTresci;
            
            using (ISession session = NhibernateSession.OpenSession())  
            {
                TTresci = session.Query<TypTresci>().ToList(); 
            }
            return View(TTresci);
        }

        public ActionResult Details(int id)
        {
            TypTresci TTresc = new TypTresci();
            using (ISession session = NhibernateSession.OpenSession())
            {
                TTresc = session.Query<TypTresci>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(TTresc);
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
                TypTresci TTresc = new TypTresci();                                     
                TTresc.nazwa = nazwa;
                using (ISession session = NhibernateSession.OpenSession())
                {

                    using (ITransaction transaction = session.BeginTransaction()) 
                    {
                        session.Save(TTresc);
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
            TypTresci TTresc = new TypTresci();
            using (ISession session = NhibernateSession.OpenSession())
            {
                TTresc = session.Query<TypTresci>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(TTresc);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                TypTresci TTresc = new TypTresci();
                TTresc.id = id;
                TTresc.nazwa = collection["Nazwa"].ToString();
                using (ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(TTresc);
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
            TypTresci TTresc = new TypTresci();
            using (ISession session = NhibernateSession.OpenSession())
            {
                TTresc = session.Query<TypTresci>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", TTresc);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    TypTresci TTresc = session.Get<TypTresci>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(TTresc);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message + "##ZRODLO##" + e.Source + "##DALEJ##" + e.InnerException;
                return RedirectToAction("Index");
            }
        }
    }
}