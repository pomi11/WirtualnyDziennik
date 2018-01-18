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
          KeyReference(x => x.KlasaUczen, "KlasaUczen_id").
          KeyReference(x => x.PlanLekcji, "PlanLekcji_id");
            Map(x => x.data);
            Map(x => x.obecnosc);
            Map(x => x.ocena);
            References(x => x.TypOceny);
            Table("DziennikUcznia");
        }
    }
}