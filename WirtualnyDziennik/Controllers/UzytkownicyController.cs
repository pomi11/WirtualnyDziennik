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
            List<Uzytkownicy> Uzytkownicy;

            using (ISession session = NhibernateSession.OpenSession())
            {
                Uzytkownicy = session.Query<Uzytkownicy>().ToList();
            }

            return View(Uzytkownicy);
        }

        public ActionResult Details(int id)
        {
            Uzytkownicy Uzytkownicy = new Uzytkownicy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Uzytkownicy = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(Uzytkownicy);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Uzytkownicy model)
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
            Uzytkownicy Uzytkownicy = new Uzytkownicy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Uzytkownicy = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(Uzytkownicy);
        }

        [HttpPost]
        public ActionResult Edit(int id, Uzytkownicy model)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Uzytkownicy Uzytkownicy = session.Get<Uzytkownicy>(id);
                    Uzytkownicy.nazwa = model.nazwa;
                    Uzytkownicy.haslo = model.haslo;
                    Uzytkownicy.pesel = model.pesel;
                    Uzytkownicy.nazwisko = model.nazwisko;
                    Uzytkownicy.imie = model.imie;
                    Uzytkownicy.kod = model.kod;
                    Uzytkownicy.miasto = model.miasto;
                    Uzytkownicy.ulica = model.ulica;
                    Uzytkownicy.numer = model.numer;
                    Uzytkownicy.numer_m = model.numer_m;
                    Uzytkownicy.miejsce_kod = model.miejsce_kod;
                    Uzytkownicy.miejsce_ur = model.miejsce_ur;
                    Uzytkownicy.data_ur = model.data_ur;
                    Uzytkownicy.TypUzytkownika = model.TypUzytkownika;
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        session.SaveOrUpdate(Uzytkownicy);
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
            Uzytkownicy Uzytkownicy = new Uzytkownicy();
            using (ISession session = NhibernateSession.OpenSession())
            {
                Uzytkownicy = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", Uzytkownicy);
        }

        [HttpPost]
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
        }
    }
}