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

        [Route("api/forms")]
        [HttpGet]
        public IEnumerable<FormSourceData> GetForms()
        {
            var Forms = _context.FormSourceData;            
            return Forms.ToList();
        }

      /*  [Route("api/forms/{guid}")]
        [HttpGet]
        public String GetFormCode(string guid)
        {

        }
        */
        [Route("api/forms/{formTitle}")]
        [HttpPut]
        public int CreateForm(string formTitle)
        {
            FormSourceData form = new FormSourceData();
            form.FormName = formTitle;
            form.FormSourceDataGuid = Guid.NewGuid();
            form.CreatedDate = DateTime.Now;
            form.DeletedDate = null;
            form.FormDescription = "default description";
            

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.FormSourceData.Add(form);
            _context.SaveChanges();

            int formID = _context.FormSourceData.Max(p => p.Id);
            
            return formID;
        }



        [Route("api/forms/settings/{id}")]
        [HttpPut]
        public int SaveFormCode(int id, FormSourceData code)
        {
             
            var form =  _context.FormSourceData.Where(f => f.Id == id).FirstOrDefault();

            form.FormSourceCode = code.FormSourceCode;
            _context.SaveChanges();

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
              
            return id;
        }
    }
}

