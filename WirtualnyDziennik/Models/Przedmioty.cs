using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class Przedmioty
    {
        public virtual int id { get; set; }
        public virtual String nazwa { get; set; }
        public virtual int nauczyciel_id { get; set; }
    }
}