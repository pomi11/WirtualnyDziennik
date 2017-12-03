using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class RodzicUczen
    {
        public virtual int rodzic_id { get; set; }
        public virtual int uczen_id { get; set; }
    }
}