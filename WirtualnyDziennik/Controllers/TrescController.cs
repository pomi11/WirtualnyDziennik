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
                tresc = session.Query<Tresc>().Where(b => b.TypTresci.id==1).ToList();
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
        {/*
            using (ISession session = NhibernateSession.OpenSession())
            {
               /* List<TypTresci> jakas = session.Query<TypTresci>().ToList();
                WirtualnyDziennik.Models.TypTresci.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    WirtualnyDziennik.Models.TypTresci.ListaDostepnych.Add(Item);
                }
            }*/

            return View();
        }

        [HttpPost]
        public ActionResult Create(Tresc model)
        {
            
            try
            {

                using (ISession session = NhibernateSession.OpenSession())
                {
                    model.TypTresci = (TypTresci)session.Load("TypTresci", 1);
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

        public ActionResult Message()
        {
            using (ISession session = NhibernateSession.OpenSession())
            {
             /*   List<TypTresci> jakas = session.Query<TypTresci>().ToList();
                Tresc.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Tresc.ListaDostepnych.Add(Item);
                }*/
            }

            return View();
        }

        [HttpPost]
        public ActionResult Message(Tresc model)
        {   
            
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    UserProfileSessionData uspd = this.Session["UserProfile"] as UserProfileSessionData;
                    model.OD = uspd.Name;
                    model.TypTresci = (TypTresci)session.Load("TypTresci", 2);
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();
                    }
                }
                return View();
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
                List<TypTresci> jakas = session.Query<TypTresci>().ToList();
                WirtualnyDziennik.Models.TypTresci.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                for(int i=0;i<jakas.Count();i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    TypTresci.ListaDostepnych.Add(Item);
                }
                tresc = session.Query<Tresc>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(tresc);
        }

        [HttpPost]
        public ActionResult Edit(int id, Tresc model)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Tresc tresc = session.Get<Tresc>(id);
                    tresc.tytul = model.tytul;
                    tresc.tresc = model.tresc;
                    tresc.TypTresci = model.TypTresci;
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
            List<Tresc> Lista = new List<Tresc>();
            using (ISession session = NhibernateSession.OpenSession())
            {
                /* tresc = session.Query<Tresc>().Where(b => b.id == id).FirstOrDefault();
                 List<TypTresci> jakas = session.Query<TypTresci>().ToList();
                 WirtualnyDziennik.Models.TypTresci.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                 for (int i = 0; i < jakas.Count(); i++)
                 {
                     SelectListItem Item = new SelectListItem();
                     Item.Text = jakas[i].nazwa;
                     WirtualnyDziennik.Models.TypTresci.ListaDostepnych.Add(Item);
                 }*/
                tresc = session.Query<Tresc>().Where(b => b.id == id).FirstOrDefault();
                using (ITransaction trans = session.BeginTransaction())
                {
                    session.Delete(tresc);
                    trans.Commit();
                }
                Lista= session.Query<Tresc>().ToList();
            }
           // ViewBag.SubmitAction = "Confirm delete";
            return RedirectToAction("Index","Tresc");
        }

      /*  [HttpPost]
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
        }*/

        public ActionResult Retrieve(int id)
        {
            Uzytkownicy Odbiorca = new Uzytkownicy();
            List<Tresc> Dostepne;
            using (ISession session = NhibernateSession.OpenSession())
            {
                Odbiorca = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
                Dostepne = session.Query<Tresc>().Where(b => b.DO == Odbiorca.nazwa).Where(b => b.TypTresci.id == 2).ToList();
            }
            return View(Dostepne);
        }
    }
}