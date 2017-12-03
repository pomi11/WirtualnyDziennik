using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class UzytkownicyMap : ClassMap<Uzytkownicy>
    {
        public UzytkownicyMap()
        {
            Id(x => x.id).GeneratedBy.Increment();
            Map(x => x.nazwa);
            Map(x => x.haslo);
            Map(x => x.imie);
            Map(x => x.pesel);
            Map(x => x.nazwisko);
            Map(x => x.kod);
            Map(x => x.miasto);
            Map(x => x.ulica);
            Map(x => x.numer);
            Map(x => x.numer_m);
            Map(x => x.miejsce_kod);
            Map(x => x.miejsce_ur);
            Map(x => x.data_ur);
            References(x => x.TypUzytkownika);
            Table("Uzytkownicy");
        }
    }
}