using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    public class ProcessesController : Controller
    {
        // GET: Processes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
    }
}