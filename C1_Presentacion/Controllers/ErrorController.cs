using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C1_Presentacion.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error(String error)
        {
            ViewBag.mensaje = error;
            return View();

        }
        [HttpPost]
        public ActionResult Error()
        {

            return View();
        }
	}
}