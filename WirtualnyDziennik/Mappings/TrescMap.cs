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
            Id(x => x.id).GeneratedBy./*Increment();*/ SequenceIdentity("TRESC_ID_SEQ");
            Map(x => x.tytul);
            Map(x => x.tresc);
            Map(x => x.OD);
            Map(x => x.DO);
            References(x => x.TypTresci);
            Table("Tresc");

        }
    }
}