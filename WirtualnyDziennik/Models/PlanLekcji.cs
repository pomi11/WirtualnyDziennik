using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class PlanLekcji
    {
        public virtual int klasa_id { get; set; }
        public virtual int przedmiot_id { get; set; }
        public virtual int planlekcji_id { get; set; }
        public virtual DateTime data { get; set; }
}
}