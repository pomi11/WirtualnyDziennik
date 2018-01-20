using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class UzytkownicyController : Controller
    {
        public ActionResult Index()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Uzytkownicy> Uzytkownicy;

            using (ISession session = NhibernateSession.OpenSession())
            {
                Uzytkownicy = session.Query<Uzytkownicy>().ToList();
            }

            return View(Uzytkownicy);
        }

        public ActionResult Details(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Uzytkownicy Uzytkownicy = new Uzytkownicy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Uzytkownicy = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(Uzytkownicy);
        }

        public ActionResult Create()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<TypUzytkownika> jakas = session.Query<TypUzytkownika>().ToList();
                Uzytkownicy.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    Uzytkownicy.ListaDostepnych.Add(Item);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Uzytkownicy model)
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
                ViewBag.Message = e.Message+"##ZRODLO##"+e.Source+"##DALEJ##"+e.InnerException;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Uzytkownicy Uzytkownicy = new Uzytkownicy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<TypUzytkownika> jakas = session.Query<TypUzytkownika>().ToList();
                Uzytkownicy.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    Uzytkownicy.ListaDostepnych.Add(Item);
                }
                Uzytkownicy = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
            }
            
            ViewBag.SubmitAction = "Save";
            return View(Uzytkownicy);
        }

        [HttpPost]
        public ActionResult Edit(int id, Uzytkownicy model)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Uzytkownicy Uzytkownicy = session.Get<Uzytkownicy>(id);
                        if(model.haslo!=null) Uzytkownicy.haslo = model.haslo;
                        if(model.pesel!=0) Uzytkownicy.pesel = model.pesel;
                        if (model.nazwisko != null) Uzytkownicy.nazwisko = model.nazwisko;
                    if (model.imie != null) Uzytkownicy.imie = model.imie;
                    if (model.kod != 0) Uzytkownicy.kod = model.kod;
                    if (model.miasto != null) Uzytkownicy.miasto = model.miasto;
                    if (model.ulica != null) Uzytkownicy.ulica = model.ulica;
                    if (model.numer != 0) Uzytkownicy.numer = model.numer;
                    if (model.numer_m != 0) Uzytkownicy.numer_m = model.numer_m;
                    if (model.miejsce_kod != 0) Uzytkownicy.miejsce_kod = model.miejsce_kod;
                    if (model.miejsce_ur != null) Uzytkownicy.miejsce_ur = model.miejsce_ur;
                    if (model.data_ur != null) Uzytkownicy.data_ur = model.data_ur;
                        if(Uzytkownicy.typu.id==1) Uzytkownicy.typu = (TypUzytkownika)session.Load("TypUzytkownika",model.typu.id);
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        session.SaveOrUpdate(Uzytkownicy);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index","Home");
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
            Uzytkownicy Uzytkownicy = new Uzytkownicy();
            List<Uzytkownicy> Lista = new List<Uzytkownicy>();
            using (ISession session = NhibernateSession.OpenSession())
            {
              //  List<TypUzytkownika> jakas = session.Query<TypUzytkownika>().ToList();
             //   Uzytkownicy.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
             //   for (int i = 0; i < jakas.Count(); i++)
             //   {
            //        SelectListItem Item = new SelectListItem();
            //        Item.Text = jakas[i].nazwa;
            //        Item.Value = jakas[i].id.ToString();
           //         Uzytkownicy.ListaDostepnych.Add(Item);
           //     }
                Uzytkownicy = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
                using (ITransaction Transaction = session.BeginTransaction())
                {
                    session.Delete(Uzytkownicy);
                    Transaction.Commit();
                }
                Lista = session.Query<Uzytkownicy>().ToList();
            }
            //ViewBag.SubmitAction = "Confirm delete";
            return View("Index");
        }

       /* [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Uzytkownicy Uzytkownicy = session.Get<Uzytkownicy>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(Uzytkownicy);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }*/
    }
}