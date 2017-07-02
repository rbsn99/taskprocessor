using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

using System.Net.Mail;
using System.Text;

namespace Vidly.Controllers.api
{
    public class FormController : ApiController
    {
        private ApplicationDbContext _context;


        public FormController()
        {
            _context = new ApplicationDbContext();
        }
    }
}

