using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WirtualnyDziennik.Models;

namespace WirtualnyDziennik.Mappings
{
    public class PlanLekcjiMap : ClassMap<PlanLekcji> // do zmiany chyba cała tabela lub utworzenie jeszcze jednej
    {
        public PlanLekcjiMap()
        {
            Id(x => x.planlekcji_id).GeneratedBy./* Increment();*/SequenceIdentity("PLANLEKCJI_PLANLEKCJI_ID_SEQ");
            Map(x => x.data);
            References(x => x.Klasa);
            References(x => x.Przedmiot);
            Table("PlanLekcji");
        }
    }
}