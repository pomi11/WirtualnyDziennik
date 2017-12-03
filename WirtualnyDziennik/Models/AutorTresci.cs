using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class AutorTresci
    {
        public virtual Uzytkownicy Uzytkownicy { get; set; }
        public virtual Tresc Tresc { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            AutorTresci id;
            id = (AutorTresci)obj;
            if (id == null)
                return false;
            if (Uzytkownicy.id == id.Uzytkownicy.id && Tresc.id == id.Tresc.id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (Uzytkownicy.id + "|" + Tresc.id).GetHashCode();
        }
    }
  
}