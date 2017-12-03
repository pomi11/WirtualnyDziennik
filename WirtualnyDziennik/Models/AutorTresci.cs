using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class AutorTresci
    {
        public virtual int uzytkownik_id { get; set; }
        public virtual int tresc_id { get; set; }
    }
}