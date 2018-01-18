using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class PrzedmiotyController : Controller
    {
        public ActionResult Index(int id)
        {
            List<Przedmioty> Przedmioty;

            using (ISession session = NhibernateSession.OpenSession())
            {
                Przedmioty = session.Query<Przedmioty>().Where(b=>b.Nauczyciel.id==id).ToList();
                ViewData["nauczycielid"] = id;
            }

            return View(Przedmioty);
        }

        public ActionResult Details(int id)
        {
            Przedmioty Przedmioty = new Przedmioty();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Przedmioty = session.Query<Przedmioty>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(Przedmioty);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Przedmioty model)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
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
            Przedmioty Przedmioty = new Przedmioty();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Przedmioty = session.Query<Przedmioty>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(Przedmioty);
        }

        [HttpPost]
        public ActionResult Edit(int id, Przedmioty model)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Przedmioty Przedmioty = session.Get<Przedmioty>(id);
                    Przedmioty.nazwa = model.nazwa;
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        session.SaveOrUpdate(Przedmioty);
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
            Przedmioty Przedmioty = new Przedmioty();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Przedmioty = session.Query<Przedmioty>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", Przedmioty);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Przedmioty Przedmioty = session.Get<Przedmioty>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(Przedmioty);
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