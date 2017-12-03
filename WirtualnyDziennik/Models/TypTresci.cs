using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class TypTresci
    {
        public virtual int id { get; set; }
        public virtual String nazwa { get; set; }
        public virtual IList<Tresc> Costam { get; set; }
    }
}