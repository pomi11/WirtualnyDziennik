using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class Grade
    {
        public virtual string Uczen { get; set; }
        public virtual string Przedmiot { get; set; }
        public virtual float Ocena { get; set; }
        public virtual DateTime Data { get; set; }
    }
}