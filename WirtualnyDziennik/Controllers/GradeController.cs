using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Controllers
{
    public class GradeController : Controller
    {
        // GET: Grade
        public ActionResult Index(int id)
        {
            IList<Object[]> lista;
            using (ISession session = NhibernateSession.OpenSession())
            {
                //Odbiorca = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
                
                IQuery s= session.CreateSQLQuery("select uzytkownicy.nazwa as UCZEN, dziennikucznia.ocena, przedmioty.nazwa as PRZEDMIOT, dziennikucznia.data as DATA from KlasaUczen, Klasy, planlekcji, przedmioty, dziennikucznia, uzytkownicy where uzytkownicy.id = klasauczen.uzytkownik_id and klasauczen.klasa_id = klasy.id and klasy.id = planlekcji.klasa_id  and planlekcji.planlekcji_id = dziennikucznia.planlekcji_id and dziennikucznia.klasauczen_id = klasauczen.klasauczen_id");
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