using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class ProcessesController : ApiController
    {
        private ApplicationDbContext _context;

 
        public ProcessesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/processes
        [Route("api/getprocesses")]
        [HttpGet]
        public IEnumerable<Process> GetProcesses()
        {

            return _context.Processes.ToList();
        }

        // GET /api/processes/1

        public Process GetProcess(int id)
        {
            var process = _context.Processes.SingleOrDefault(c => c.Id == id);

            if (process == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return process;
        }

        // POST /api/processes
        [HttpPost]
        public Process CreateProcess(Process process)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Processes.Add(process);
            _context.SaveChanges();

            return process;
        }

        // PUT/api/processes/1
        [HttpPut]
        public void UpdateProcess(int id, Process process)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var processInDb = _context.Processes.SingleOrDefault(p => p.Id == id);
            if (processInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            processInDb.ProcessName = process.ProcessName;

            _context.SaveChanges();
        }

        public void DeleteProcess(int id)
        {
            var processInDb = _context.Processes.SingleOrDefault(p => p.Id == id);
            if (processInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Processes.Remove(processInDb);
            _context.SaveChanges();

        }
    }
}
