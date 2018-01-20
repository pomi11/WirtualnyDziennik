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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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

        public ActionResult IndexAdmin()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Klasy> klasy;
            List<String> nazwiska = new List<String>();
           
           
            using (ISession session = NhibernateSession.OpenSession())
            {
                klasy = session.Query<Klasy>().ToList();                
                for(int i = 0;i<klasy.Count();i++)
                {
                    nazwiska.Add(klasy[i].Wychowawca.nazwisko);
                }
            }

            return View(klasy);
        }

        public ActionResult Details(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Klasy klasy = new Klasy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                klasy = session.Query<Klasy>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(klasy);
        }

        public ActionResult Create()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Klasy model)
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
        public ActionResult Add(int id)
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
                    Przedmioty.ListaDostepnychNauczycieli.Add(Item);
                }
                ViewData["klasa"] = id;
                ViewBag.SubmitAction = "Save";
            }
            return View();
        }
[HttpPost]
 public ActionResult Add(KlasaUczen model)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KlasaUczen ku= new KlasaUczen();
            
           
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

public ActionResult EditStudents(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Klasy klasy = new Klasy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<KlasaUczen> klasauczen = session.Query<KlasaUczen>().Where(b => b.Klasa.id == id).ToList();
                IList<Object[]> s = session.CreateSQLQuery("select  u.* from uzytkownicy u,klasauczen where u.id=klasauczen.uzytkownik_id and klasauczen.klasa_id=" + id).List<Object[]>();
                List<Uzytkownicy> jakas = session.Query<Uzytkownicy>().Where(c => c.typu.id == 3).ToList();
                WirtualnyDziennik.Models.Przedmioty.ListaDostepnychNauczycieli = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < s.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = s[i][1].ToString();
                    Item.Value = s[i][0].ToString();
                    Przedmioty.ListaDostepnychNauczycieli.Add(Item);
                }
                klasy = session.Query<Klasy>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewData["klasa"] = id;
            ViewBag.SubmitAction = "Save";
            return View(klasy);
        }

        public ActionResult Edit(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Klasy klasy = new Klasy();
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
                klasy = session.Query<Klasy>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(klasy);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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