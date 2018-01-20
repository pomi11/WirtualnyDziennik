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
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserProfileSessionData upsd = Session["UserProfile"] as UserProfileSessionData;
            IList<Object[]> lista;
            WirtualnyDziennik.Models.Przedmioty.ListaDostepnych = new List<SelectListItem>();

            IQuery s = null;
            using (ISession session = NhibernateSession.OpenSession())
            {
                //Odbiorca = session.Query<Uzytkownicy>().Where(b => b.id == id).FirstOrDefault();
                if (upsd.Typ == "UCZEN")
                {
                    s = session.CreateSQLQuery("select dziennikucznia.ocena,typoceny.nazwa,przedmioty.nazwa as PRZEDMIOT, TO_CHAR(dziennikucznia.data,'DD/MM/YYYY') as DATA from typoceny, uzytkownicy, klasauczen, dziennikucznia, planlekcji, przedmioty where dziennikucznia.ocena!=0 and uzytkownicy.id =" + id + " and uzytkownicy.id = klasauczen.uzytkownik_id and klasauczen.klasauczen_id = dziennikucznia.klasauczen_id and typoceny.id=dziennikucznia.typoceny_id and dziennikucznia.planlekcji_id = planlekcji.planlekcji_id and planlekcji.przedmiot_id = przedmioty.id");
                }
                if(upsd.Typ=="RODZIC")
                {
                    s = session.CreateSQLQuery("select uzytkownicy.imie as Imie ,uzytkownicy.nazwisko as Nazwisko ,dziennikucznia.ocena,typoceny.nazwa,przedmioty.nazwa as PRZEDMIOT, TO_CHAR(dziennikucznia.data,'DD/MM/YYYY') as DATA from typoceny,uzytkownicy, klasauczen, dziennikucznia, planlekcji, przedmioty where dziennikucznia.ocena!=0 and uzytkownicy.id in (SELECT u.id FROM uzytkownicy u WHERE EXISTS(SELECT NULL FROM rodzicuczen ru WHERE u.id = ru.uczen_id AND ru.rodzic_id =" + id+")) and uzytkownicy.id = klasauczen.uzytkownik_id and typoceny.id=dziennikucznia.typoceny_id and klasauczen.klasauczen_id = dziennikucznia.klasauczen_id and dziennikucznia.planlekcji_id = planlekcji.planlekcji_id and planlekcji.przedmiot_id = przedmioty.id");
                }
               
                lista = s.List<Object[]>();
             }
            return View(lista);
        }
    }
}