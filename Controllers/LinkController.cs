using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoRedesDefinitivo.Controllers;

namespace ProyectoRedesDefinitivo.Controllers
{
    public class LinkController : Controller
    {
        // GET: Link
        public ActionResult Link()
        {
            Link obj = new Link();
            obj.link1 = Request.Form["link1"].ToString();
            obj.link2 = Request.Form["link2"].ToString();
            obj.analizador();


            return View(obj);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}