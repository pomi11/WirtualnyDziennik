using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class PrzedmiotyMap : ClassMap<Przedmioty>
    {
        public PrzedmiotyMap()
        {
            Id(x => x.id).GeneratedBy.Increment();
            Map(x => x.nazwa);
            References(x => x.Nauczyciel);
            Table("Przedmioty");
        }
    }
}