using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    public class InstanceController : Controller
    {
        // GET: Instance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewRequest()
        {
            return View();
        }
        public ActionResult InstanceDetails()
        {
            return View();
        }
    }
}