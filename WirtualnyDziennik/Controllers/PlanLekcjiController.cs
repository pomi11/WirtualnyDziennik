using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyDziennik.Models;
namespace WirtualnyDziennik.Controllers
{
    public class PlanLekcjiController : Controller // do zmiany chyba cała tabela lub utworzenie jeszcze jednej
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}