using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class Klasy
    {
        public virtual int id { get; set; }
        public virtual String nazwa { get; set; }
        public virtual Uzytkownicy Wychowawca { get; set; } // trzeba dodać jakieś ograniczenie, że tylko TYP UZYTKOWNIKA NAUCZYCIEL
    }
}