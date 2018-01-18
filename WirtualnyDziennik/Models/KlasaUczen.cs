using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class KlasaUczen
    {
        //public virtual int uzytkownik_id { get; set; }
      //  public virtual int klasa_id { get; set; }
        public virtual int klasauczen_id { get; set; }
        public virtual Uzytkownicy Uzytkownik { get; set; }
        public virtual Klasy Klasa { get; set; }
}
}