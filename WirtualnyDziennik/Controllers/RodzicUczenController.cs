using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class RodzicUczenController : Controller
    {
        // GET: RodzicUczen
        public ActionResult Index()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Uzytkownicy> rodzice;

            using (ISession session = NhibernateSession.OpenSession())
            {
                rodzice = session.Query<Uzytkownicy>().Where(c=>c.typu.id==4).ToList();
            }

            return View(rodzice);
        }

        public ActionResult PrzypiszUcznia(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<Uzytkownicy> jakas = session.Query<Uzytkownicy>().Where(c => c.typu.id == 2).ToList();
                WirtualnyDziennik.Models.Przedmioty.ListaDostepnychNauczycieli = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwisko;
                    Item.Value = jakas[i].id.ToString();
                    WirtualnyDziennik.Models.Przedmioty.ListaDostepnychNauczycieli.Add(Item);
                }
            }
            ViewData["rodzicid"] = id;
            ViewBag.SubmitAction = "Save";
            return View();
        }

        [HttpPost]
        public ActionResult PrzypiszUcznia(RodzicUczen model)
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
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message + "##ZRODLO##" + e.Source + "##DALEJ##" + e.InnerException;
                return View();
            }
        }
    }
}