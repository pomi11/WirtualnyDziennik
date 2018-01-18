using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class TypOcenyMap : ClassMap<TypOceny>
    {
        public TypOcenyMap()
        {
            Id(x => x.id).GeneratedBy./* Increment();*/SequenceIdentity("TYPOCENY_ID_SEQ");
            Map(x => x.nazwa);
            //  HasMany(x => x.Costam).KeyColumn("typtresci_id").Inverse().Cascade.All();
            Table("TypOceny");
        }
    }
}