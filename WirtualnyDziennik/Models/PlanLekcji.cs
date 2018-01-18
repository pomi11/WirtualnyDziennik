using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class PlanLekcji // do zmiany chyba cała tabela lub utworzenie jeszcze jednej
    {
        public virtual int planlekcji_id { get; set; }
        public virtual Klasy Klasa { get; set; }
        public virtual Przedmioty Przedmiot { get; set; }
        public virtual DateTime data { get; set; }
}
}