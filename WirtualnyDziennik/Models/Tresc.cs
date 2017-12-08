using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class Tresc
    {
        public virtual int id { get; set; }
        public virtual String tytul { get; set; }
        public virtual String tresc { get; set; }
        public virtual String OD { get; set; }
        public virtual String DO { get; set; }
        //public virtual int typtresci_id { get; set; }
        public virtual TypTresci TypTresci { get; set; }
        public static IList<System.Web.Mvc.SelectListItem> ListaDostepnych { get; set; }
    }
}