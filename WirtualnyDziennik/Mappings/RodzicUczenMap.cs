using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;
namespace WirtualnyDziennik.Mappings
{
    public class RodzicUczenMap : ClassMap<RodzicUczen>
    {
        public RodzicUczenMap()
        {
            CompositeId().
        KeyReference(x => x.rodzic, "rodzic_id").
        KeyReference(x => x.uczen, "uczen_id");
            //  References(x => x.rodzic);
            //   References(x => x.uczen);
            Table("RodzicUczen");
        }
    }
}