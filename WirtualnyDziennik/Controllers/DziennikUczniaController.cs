using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class DziennikUczniaController : Controller
    {
        // GET: DziennikUcznia
        public ActionResult Index(int iducznia,int planlekcjiid)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ISession session = NhibernateSession.OpenSession())
            {
                DziennikUcznia.Obecnosci = new List<SelectListItem>();
                SelectListItem Obecnosc = new SelectListItem();
                int value = 0;
                Obecnosc.Value = value.ToString();
                Obecnosc.Text = "OBECNY";
                DziennikUcznia.Obecnosci.Add(Obecnosc);
                SelectListItem Obecnosc1 = new SelectListItem();
                value = 2;
                Obecnosc1.Value = value.ToString();
                Obecnosc1.Text = "NIEOBECNY";
                DziennikUcznia.Obecnosci.Add(Obecnosc1);
                SelectListItem Obecnosc2 = new SelectListItem();
                value = 1;
                Obecnosc2.Value = value.ToString();
                Obecnosc2.Text = "SPOZNIONY";
                DziennikUcznia.Obecnosci.Add(Obecnosc2);
                List<TypOceny> jakas = session.Query<TypOceny>().ToList();
                TypOceny.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();
                DziennikUcznia dziennik = new DziennikUcznia();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    SelectListItem Item = new SelectListItem();
                    Item.Text = jakas[i].nazwa;
                    Item.Value = jakas[i].id.ToString();
                    TypOceny.ListaDostepnych.Add(Item);
                }
                //KlasaUczen kl = session.Query<KlasaUczen>().Where(b => b.Uzytkownik.id == iducznia).First();

              //  dziennik.KlasaUczen = kl;
            }
            return View();
        }

        public ActionResult Insert(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(int iducznia,int planlekcjiid,DziennikUcznia model)
        {

            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    model.KlasaUczen = session.Query<KlasaUczen>().Where(b => b.Uzytkownik.id == iducznia).First();
                    model.PlanLekcji = (PlanLekcji)session.Load("PlanLekcji", planlekcjiid);
                    // model.PlanLekcji = session.Query<PlanLekcji>().Where(c => c.planlekcji_id == planlekcjiid).First();
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index",new { iducznia = iducznia, planlekcjiid = planlekcjiid });
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message + "##ZRODLO##" + e.Source + "##DALEJ##" + e.InnerException;
                return View();
            }
        }
    }
}