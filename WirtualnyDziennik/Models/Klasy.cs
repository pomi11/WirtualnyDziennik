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
        public virtual int wychowawca_id { get; set; }
    }
}