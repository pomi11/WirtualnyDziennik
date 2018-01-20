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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Przedmioty> Przedmioty;

            using (ISession session = NhibernateSession.OpenSession())
            {
                Przedmioty = session.Query<Przedmioty>().Where(b=>b.Nauczyciel.id==id).ToList();
                ViewData["nauczycielid"] = id;
            }

            return View(Przedmioty);
        }
        public ActionResult IndexAdmin()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<Przedmioty> tresc;
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<Uzytkownicy> jakas = session.Query<Uzytkownicy>().Where(c => c.typu.id == 3).ToList();
                WirtualnyDziennik.Models.Przedmioty.ListaDostepnychNauczycieli = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    Przedmioty.ListaDostepnychNauczycieli.Add(Item);
                }
                tresc = session.Query<Przedmioty>().ToList();

            }
            return View(tresc); 
        }

        public ActionResult Details(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Przedmioty Przedmioty = new Przedmioty();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Przedmioty = session.Query<Przedmioty>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(Przedmioty);
        }

        public ActionResult Create()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<Przedmioty> tresc;
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<Uzytkownicy> jakas = session.Query<Uzytkownicy>().Where(c => c.typu.id == 3).ToList();
                WirtualnyDziennik.Models.Przedmioty.ListaDostepnychNauczycieli = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    Przedmioty.ListaDostepnychNauczycieli.Add(Item);
                }
                tresc = session.Query<Przedmioty>().ToList();

            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Przedmioty model)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                return RedirectToAction("IndexAdmin");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                return RedirectToAction("IndexAdmin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                return RedirectToAction("IndexAdmin");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}