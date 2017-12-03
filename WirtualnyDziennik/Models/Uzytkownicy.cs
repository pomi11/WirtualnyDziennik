using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class Uzytkownicy
    {
        public virtual int id { get; set; }
        public virtual String nazwa { get; set; }
        public virtual String haslo { get; set; }
        public virtual int pesel { get; set; }
        public virtual String nazwisko { get; set; }
        public virtual String imie { get; set; }
        public virtual int kod { get; set; }
        public virtual String miasto { get; set; }
        public virtual String ulica { get; set; }
        public virtual int numer { get; set; }
        public virtual int numer_m { get; set; }
        public virtual int miejsce_kod { get; set; }
        public virtual String miejsce_ur { get; set; }
        public virtual DateTime data_ur{ get; set; }
        public virtual TypUzytkownika TypUzytkownika { get; set; }
}
}