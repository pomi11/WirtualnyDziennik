using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class TrescMap : ClassMap<Tresc>
    {
        public TrescMap()
        {
            Id(x => x.id);
            Map(x => x.tytul);
            Map(x => x.tresc);
            Map(x => x.typtresci_id);
            Table("Tresc");
            References(x => x.TTresci, "typtresci_id").Cascade.None();

        }
    }
}