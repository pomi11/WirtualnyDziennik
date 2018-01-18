using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class TypOceny
    {
        public virtual int id { get; set; }
        public virtual String nazwa { get; set; }
        public static List<System.Web.Mvc.SelectListItem> ListaDostepnych { get; set; }
    }
}