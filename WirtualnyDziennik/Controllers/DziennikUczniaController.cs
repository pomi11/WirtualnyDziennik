using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WirtualnyDziennik.Controllers
{
    public class DziennikUczniaController : Controller
    {
        // GET: DziennikUcznia
        public ActionResult Index()
        {
            return View();
        }
    }
}