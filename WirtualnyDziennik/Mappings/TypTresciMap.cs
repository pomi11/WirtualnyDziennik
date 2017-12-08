using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class TypTresciMap : ClassMap<TypTresci>
    {
        public TypTresciMap()
        {
            Id(x => x.id).GeneratedBy./* Increment();*/SequenceIdentity("TYPTRESCI_ID_SEQ");
            Map(x => x.nazwa);
          //  HasMany(x => x.Costam).KeyColumn("typtresci_id").Inverse().Cascade.All();
            Table("TypTresci");
        }
    }
}