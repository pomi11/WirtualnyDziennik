using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class AutorTresciMap : ClassMap<AutorTresci>
    {
        public AutorTresciMap()
        {
            CompositeId().
        KeyReference(x => x.Uzytkownicy, "Uzytkownicy").
        KeyReference(x => x.Tresc, "Tresc");
            //References(x => x.Uzytkownicy);
            //References(x => x.Tresc);
            Table("AutorTresci");
        }


    }
}