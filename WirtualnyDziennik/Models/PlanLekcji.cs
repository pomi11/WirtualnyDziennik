using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    public class PlanLekcji // do zmiany chyba cała tabela lub utworzenie jeszcze jednej
    {
        public virtual int planlekcji_id { get; set; }
        public virtual Klasy Klasa { get; set; }
        public virtual Przedmioty Przedmiot { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public virtual DateTime data { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public static DateTime asda { get; set; }
}
}