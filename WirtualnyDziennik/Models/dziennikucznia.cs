using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class DziennikUcznia
    {
      // public virtual int klasauczen_id { get; set; }
        public virtual KlasaUczen KlasaUczen { get; set; }
        public virtual PlanLekcji PlanLekcji { get; set; }
        //public virtual int planlekcji_id { get; set; }
        public virtual DateTime data { get; set; }
        public virtual int obecnosc { get; set; }
        public virtual float ocena { get; set; }
        public virtual int typ_oceny { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            DziennikUcznia id;
            id = (DziennikUcznia)obj;
            if (id == null)
                return false;
            if (KlasaUczen.klasauczen_id == id.KlasaUczen.klasauczen_id && PlanLekcji.planlekcji_id == id.PlanLekcji.planlekcji_id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (PlanLekcji.planlekcji_id + "|" + KlasaUczen.klasauczen_id).GetHashCode();
        }
    }

    
}