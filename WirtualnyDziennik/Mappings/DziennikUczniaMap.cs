using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class DziennikUczniaMap : ClassMap<DziennikUcznia>
    {
        public DziennikUczniaMap()
        {
            //References(x => x.KlasaUczen);
            //References(x => x.PlanLekcji);
            CompositeId().
          KeyReference(x => x.KlasaUczen, "KlasaUczen").
          KeyReference(x => x.PlanLekcji, "PlanLekcji");
            Map(x => x.data);
            Map(x => x.obecnosc);
            Map(x => x.ocena);
            Map(x => x.typ_oceny);
            Table("DziennikUcznia");
        }
    }
}