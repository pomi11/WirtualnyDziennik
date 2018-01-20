using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Grade
        public ActionResult Index(int id)
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
                UserProfileSessionData upsd = Session["UserProfile"] as UserProfileSessionData;
            IList<Object[]> lista;
            IQuery s = null;
            using (ISession session = NhibernateSession.OpenSession())
            {
                //Odbiorca = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
                if (upsd.Typ == "UCZEN")
                {
                    s = session.CreateSQLQuery("select dziennikucznia.obecnosc,przedmioty.nazwa as PRZEDMIOT, TO_CHAR(dziennikucznia.data,'DD/MM/YYYY') as DATA from uzytkownicy, klasauczen, dziennikucznia, planlekcji, przedmioty where uzytkownicy.id =" + id + " and uzytkownicy.id = klasauczen.uzytkownik_id and klasauczen.klasauczen_id = dziennikucznia.klasauczen_id and dziennikucznia.planlekcji_id = planlekcji.planlekcji_id and planlekcji.przedmiot_id = przedmioty.id");
                }
                if (upsd.Typ == "RODZIC")
                {
                    
                    s = session.CreateSQLQuery("select uzytkownicy.imie as Imie ,uzytkownicy.nazwisko as Nazwisko ,dziennikucznia.obecnosc,przedmioty.nazwa as PRZEDMIOT, TO_CHAR(dziennikucznia.data,'DD/MM/YYYY') as DATA from uzytkownicy, klasauczen, dziennikucznia, planlekcji, przedmioty where uzytkownicy.id in (SELECT u.id FROM uzytkownicy u WHERE EXISTS(SELECT NULL FROM rodzicuczen ru WHERE u.id = ru.uczen_id AND ru.rodzic_id = " + id + ")) and uzytkownicy.id = klasauczen.uzytkownik_id and klasauczen.klasauczen_id = dziennikucznia.klasauczen_id and dziennikucznia.planlekcji_id = planlekcji.planlekcji_id and planlekcji.przedmiot_id = przedmioty.id");
                }
                /*lista =session.CreateSQLQuery("select uzytkownicy.nazwa as UCZEN ,dziennikucznia.ocena,przedmioty.nazwa as PRZEDMIOT,dziennikucznia.data as DATA from KlasaUczen, Klasy, planlekcji, przedmioty, dziennikucznia, uzytkownicy where uzytkownicy.id = klasauczen.uzytkownik_id and klasauczen.klasa_id = klasy.id and klasy.id = planlekcji.klasa_id  and planlekcji.planlekcji_id = dziennikucznia.planlekcji_id and dziennikucznia.klasauczen_id = klasauczen.klasauczen_id")
                     .AddScalar("UCZEN",NHibernateUtil.String)
                     .AddScalar("OCENA",NHibernateUtil.Double)
                     .AddScalar("PRZEDMIOT",NHibernateUtil.String)
                     .AddScalar("DATA",NHibernateUtil.DateTime).List<Object[]>();*/
                lista = s.List<Object[]>();
            }
            return View(lista);
        }
    }
}