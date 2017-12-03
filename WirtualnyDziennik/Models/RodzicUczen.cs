using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class RodzicUczen
    {
        public virtual Uzytkownicy rodzic { get; set; }
        public virtual Uzytkownicy uczen { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            RodzicUczen id;
            id = (RodzicUczen)obj;
            if (id == null)
                return false;
            if (rodzic.id == id.rodzic.id && uczen.id == id.uczen.id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (rodzic.id + "|" + uczen.id).GetHashCode();
        }
    }
}