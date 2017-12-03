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
            Id(x => x.id);
            Map(x => x.nazwa);
            Table("TypTresci");
        }
    }
}