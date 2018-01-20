using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;
namespace WirtualnyDziennik.Controllers
{
    public class PlanLekcjiController : Controller // do zmiany chyba cała tabela lub utworzenie jeszcze jednej
    {

        public ActionResult Index(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<Decimal> p2;
            IList<String> przedmioty;
            using (ISession session = NhibernateSession.OpenSession())
            {
                ISQLQuery s = session.CreateSQLQuery("select distinct p.nazwa from przedmioty p, klasy k,planlekcji pl where pl.klasa_id=k.id and p.id=pl.przedmiot_id and k.id=" + id);
                
                przedmioty = s.List<String>();
               
            }
            List<int> a = new List<int>();
            ViewData["klasaid"] = id;
            ViewData["lista"] = a;
            return View(przedmioty);
        }

        public ActionResult Create(int klasaid, int przedmiotid)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {

                using (ISession session = NhibernateSession.OpenSession())
                {
                    PlanLekcji pl = new PlanLekcji();
                    pl.Klasa = (Klasy)session.Load("Klasy", klasaid);
                    pl.Przedmiot = (Przedmioty)session.Load("Przedmioty", przedmiotid);
                    pl.data = DateTime.Now.Date;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(pl);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("ListaLekcji", "Classes", new { id = klasaid, przedmiotid = przedmiotid });
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message + "##ZRODLO##" + e.Source + "##DALEJ##" + e.InnerException;
                return View();
            }
        }
        public ActionResult CreateAdmin(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<Decimal> p2;
            using (ISession session = NhibernateSession.OpenSession())
            {
                ISQLQuery s1 = session.CreateSQLQuery("select distinct p.id from przedmioty p, klasy k,planlekcji pl where pl.klasa_id=k.id and p.id=pl.przedmiot_id and k.id=" + id);
                p2 = s1.List<Decimal>();
                List<Przedmioty> jakas = session.Query<Przedmioty>().ToList();
                for (int i = 0; i < jakas.Count(); i++)
                {
                    for (int j = 0; j < p2.Count();  j++)
                    {
                        if (jakas[i].id == Convert.ToInt32(p2[j]))
                        {
                            jakas.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
                    WirtualnyDziennik.Models.Przedmioty.ListaDostepnych = new List<System.Web.Mvc.SelectListItem>();

                    for (int i = 0; i < jakas.Count(); i++)
                    {
                        SelectListItem Item = new SelectListItem();
                        Item.Text = jakas[i].nazwa;
                        Item.Value = jakas[i].id.ToString();
                        Przedmioty.ListaDostepnych.Add(Item);
                    }

                }

                ViewBag.SubmitAction = "Dodaj";
                ViewData["klasaid"] = id;
                return View();
            }
        [HttpPost]
        public ActionResult CreateAdmin(int id, PlanLekcji model)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            PlanLekcji pl = new PlanLekcji();
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    model.data = DateTime.Now.Date;
                    
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index",new {id=id });
            }
            catch (Exception e)
            {
                return View();
            }
        }
        }
    }
