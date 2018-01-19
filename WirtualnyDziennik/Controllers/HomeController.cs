using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Tresc> lista = new List<Tresc>();
            using (ISession session = NhibernateSession.OpenSession())
            {
                lista = session.Query<Tresc>().Where(b => b.TypTresci.id == 1).ToList<Tresc>();
            }
                
            return View(lista);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Uzytkownicy model)
        {
            try
            {

                using (ISession session = NhibernateSession.OpenSession())
                {
                    model.typu = (TypUzytkownika)session.Load("TypUzytkownika", 2);
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
        [HttpPost]
        public ActionResult Login(string haslo,string login)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Uzytkownicy U = session.Query<Uzytkownicy>().Where(b => b.nazwa == login).FirstOrDefault();
                    if (U.haslo == haslo)
                    {
                        UserProfileSessionData UserLogged = new UserProfileSessionData
                        {
                            Name = U.nazwa,
                            UserId = U.id,
                            EmailAddress = U.email,
                            Typ = U.typu.nazwa
                        };
                        this.Session.Add("UserProfile", UserLogged);
                       // this.Session["User"] = UserLogged;
                    }
                    else return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            this.Session.Remove("UserProfile");
            return RedirectToAction("Index");
        }

        public ActionResult Remind()
        {
            return View();
        }

        [HttpPost]
        public /*async System.Threading.Tasks.Task<*/ActionResult/*>*/ Remind/*Async*/(Uzytkownicy model)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    //Uzytkownicy U = new Uzytkownicy();
                    Uzytkownicy U = session.Query<Uzytkownicy>().Where(b => b.nazwa == model.nazwa).FirstOrDefault();
                    
                    if (U.email == model.email)
                    {
                        //   using (var smtp = new SmtpClient())
                        //  {
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com",587);
                        
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential("wirtualnydziennik1@gmail.com", "******");
                        MailAddress od = new MailAddress("wirtualnydziennik1@gmail.com");
                            MailAddress Do = new MailAddress(model.email);
                            MailMessage s = new MailMessage(od,Do);

                        string message = "Twój login: " + U.nazwa + "\nTwoje haslo: " + U.haslo;
                        s.Subject = "Przypomnienie hasła";
                            s.Body = message;
                            /*await*/ smtp.Send/*MailAsync*/(s);
                     //   }
                    }
                    else throw new Exception("Głupi błąd");
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message + "##ZRODLO##" + e.Source + "##DALEJ##" + e.InnerException;
                return View();
            }
            ViewBag.Message = "Hasło zostało wysłane";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}