﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WirtualnyDziennik.Controllers
{
    public class AutorTresciController : Controller
    {
        // GET: AutorTresci
        public ActionResult Index()
        {
            if (this.Session["UserProfile"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}