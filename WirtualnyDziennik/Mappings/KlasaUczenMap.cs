using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class KlasaUczenMap : ClassMap<KlasaUczen>
    {
        public KlasaUczenMap()
        {
            Id(x => x.klasauczen_id).GeneratedBy./* Increment();*/SequenceIdentity("KLASAUCZEN_KLASAUCZEN_ID_SEQ");
            References(x => x.Uzytkownicy);
            References(x => x.Klasy);
            Table("KlasaUczen");
        }
    }
}