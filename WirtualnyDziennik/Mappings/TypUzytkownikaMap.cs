using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class TypUzytkownikaMap : ClassMap<TypUzytkownika>
    {
        public TypUzytkownikaMap()
        {
            Id(x => x.id).GeneratedBy./*Increment();*/SequenceIdentity("TYPUZYTKOWNIKA_ID_SEQ");
            Map(x => x.nazwa);
            Table("TypUzytkownika");
        }
    }
}