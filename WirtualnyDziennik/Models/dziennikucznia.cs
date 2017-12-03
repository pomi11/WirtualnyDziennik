using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class DziennikUcznia
    {
       public virtual int klasauczen_id { get; set; }
        public virtual int planlekcji_id { get; set; }
        public virtual DateTime data { get; set; }
        public virtual int obecnosc { get; set; }
        public virtual float ocena { get; set; }
        public virtual int typ_oceny { get; set; }
    }
}