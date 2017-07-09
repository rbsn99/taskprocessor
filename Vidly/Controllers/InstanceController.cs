using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class InstanceController : Controller
    {
        private ApplicationDbContext _context;

 
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
        public ActionResult OpenForm()
        {
            _context = new ApplicationDbContext();

            string guid =   Request["guid"] ;
            var itask = _context.InstanceTasks
                .Where(it => it.ItaskGuid.ToString() == guid).FirstOrDefault();

            var ptattribute = _context.ProcessTaskAttributes
                .Where(pta => pta.ProcessTaskGuid == itask.TaskGuid).FirstOrDefault();

            string formID = ptattribute.AttributeValue;

            var model = _context.FormSourceData
                .Where(f => f.Id.ToString() == formID)
                .FirstOrDefault();
 
             return View(model);
        }
    }
}