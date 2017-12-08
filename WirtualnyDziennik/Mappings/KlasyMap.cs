using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class KlasyMap : ClassMap<Klasy>
    {
        public KlasyMap()
        {
            Id(x => x.id).GeneratedBy./* Increment();*/SequenceIdentity("KLASY_ID_SEQ");
            Map(x => x.nazwa);
            References(x => x.Nauczyciel);
           // Map(x => x.Nauczyciel);
            Table("Klasy");
        }
    }
}