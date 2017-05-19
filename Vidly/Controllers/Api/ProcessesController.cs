using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
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
        [Route("api/processes")]
        [HttpGet]
        public IEnumerable<Process> GetProcesses()
        {
            var Processes = _context.Processes;
            return Processes.ToList();
        }

        // GET /api/processes/1
        [Route("api/processes/settings/{id}")]
        [HttpGet]
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

 

        [Route("api/processes/settings/{id}")]
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

 
        [Route("api/processes/tasktypes")]
        [HttpGet]
        public IEnumerable<ProcessTaskType> GetProcessTaskTypes()
        {
            var ProcessesTaskTypes = _context.ProcessTaskTypes;
            return ProcessesTaskTypes.ToList();
        }

 
        [Route("processes/settings/processtasks/{id}")]
        [HttpGet]
        public IEnumerable<PTView>  GetProcessTasks(int id)
        {
            var process = _context.Processes.SingleOrDefault(c => c.Id == id);

            List<ProcessTask> processTasks = _context.ProcessTasks.Where(c => c.ProcessGuid == process.ProcessGuid).ToList();

            List<PTView> PTview = new List<PTView>();

             foreach (ProcessTask pt in processTasks)
            {
                PTView ptv = new PTView();
                ptv.Id = pt.Id;
                ptv.ProcessTaskGuid = pt.ProcessTaskGuid;
                ptv.ProcessGuid = pt.ProcessGuid;
                ptv.Process = process;
                ptv.TaskTypeId = pt.TaskTypeId;
                ptv.ProcessTaskType = _context.ProcessTaskTypes.Where(c => c.Id == pt.TaskTypeId).SingleOrDefault();
                ptv.TaskName = pt.TaskName;
                ptv.CompletionTask = pt.CompletionTask;
                ptv.ProcessTaskRecipient = _context.ProcessTaskRecipients.Where(c => c.ProcessTaskGuid == pt.ProcessTaskGuid).SingleOrDefault();
                ptv.ProcessTaskAttributes = _context.ProcessTaskAttributes.Where(c => c.ProcessTaskGuid == pt.ProcessTaskGuid).ToList();
                ptv.CreatedDate = pt.CreatedDate;
                ptv.DeletedDate = pt.DeletedDate;

                var transitions = _context.ProcessTaskTransition
                    .Where(t => t.DestinationTaskGuid == pt.ProcessTaskGuid)
                    .ToList();
                string taskIds = string.Empty;

                foreach (var t in transitions)
                {
                    var sourceTasksId = _context.ProcessTasks
                        .Where(st => st.ProcessTaskGuid == t.SourceTaskGuid)
                        .FirstOrDefault()
                        .Id
                        .ToString();

                    taskIds += sourceTasksId + ",";
                }
                if(taskIds != string.Empty)
                    taskIds = taskIds.Remove(taskIds.Length - 1);

                ptv.ProcessTaskDependencies = taskIds;

                PTview.Add(ptv);
            }
            return PTview;
        }


        [Route("processes/settings/processtasks/{id}")]
        [HttpPut]
        public void UpdateProcessTasks(int id, PTUpdate ptu)
        {
            /* ************************ UPDATE ProcessTaskTransition ************************ */

            Guid processGuid = _context.Processes
                .Where(p => p.Id == ptu.ProcessId)
                .FirstOrDefault()
                .ProcessGuid;

            List<string> depsGiven = ptu.ProcessTaskDependencies.Trim().Split(',').ToList();
            List<string> desiredSourceTaskGuids = new List<string>();

            //check if given task IDs are already existing in transition table, or need to be added
            foreach (string dep in depsGiven) {


                //guid of source task to be completed run the destination task
                var sourceTaskGuid = _context.ProcessTasks
                    .Where(pt => pt.Id.ToString() == dep.ToString() && pt.ProcessGuid == processGuid)
                    .SingleOrDefault()
                    .ProcessTaskGuid;

                desiredSourceTaskGuids.Add(sourceTaskGuid.ToString());

                //check if this is an existing dependency
                var dependencyChcek = _context
                    .ProcessTaskTransition
                    .Where(ptt => ptt.SourceTaskGuid == sourceTaskGuid 
                            && ptt.DestinationTaskGuid == ptu.ProcessTaskGuid
                            && ptt.DeletedDate == null)
                    .ToList();


                //if none found, create new dependency
                if (dependencyChcek.Count == 0)
                {
                    ProcessTaskTransition PTT = new ProcessTaskTransition();
                    PTT.CreatedDate = DateTime.Now;
                    PTT.DeletedDate = null;
                    PTT.DestinationTaskGuid = ptu.ProcessTaskGuid;
                    PTT.SourceTaskGuid = sourceTaskGuid;
                    PTT.SourceTaskState = "Initiating";
                    PTT.ProcessTaskTransitionId = 1;
                    PTT.ProcessTaskTransitionGuid = Guid.NewGuid();

                    _context.ProcessTaskTransition.Add(PTT);
                }
            }

            //check if existing ones are to be deleted

            //get all existing source tasks guids for this destination task
            var existingSourceTasks= _context.ProcessTaskTransition
                .Where(exs => exs.DestinationTaskGuid == ptu.ProcessTaskGuid)
                .ToList();




            foreach (var existingSourceTask in existingSourceTasks)
            {
                if (!desiredSourceTaskGuids.Contains(existingSourceTask.SourceTaskGuid.ToString()))
                {
                    var transitionToRemove = _context.ProcessTaskTransition
                        .Where(ttr => ttr.DestinationTaskGuid == ptu.ProcessTaskGuid
                                        && ttr.SourceTaskGuid == existingSourceTask.SourceTaskGuid)
                        .FirstOrDefault();

                    _context.ProcessTaskTransition.Remove(transitionToRemove);
                }
            }


            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            /* ************************ UPDATE ProcessTask DATA ************************ */
            //this task object
            var task = _context.ProcessTasks.
                Where(t => t.ProcessTaskGuid == ptu.ProcessTaskGuid)
                .FirstOrDefault();

            task.TaskName = ptu.TaskName;
            
            _context.SaveChanges();
        }


        [Route("processes/settings/processtasks/newtask")]
        [HttpPost]
        public void AddNewTask(PTUpdate pt)
        {
            var processGuid = _context.Processes.Where(p => p.Id == pt.ProcessId).FirstOrDefault().ProcessGuid;
            var existingTasks = _context.ProcessTasks.Where(et => et.ProcessGuid == processGuid).ToList();

            int max = 0;

            foreach (var task in existingTasks)
            {
                if (task.Id >= 0)
                    max = task.Id;
            }
            
            ProcessTask newPT = new ProcessTask();
            newPT.Id = max + 1;
            newPT.ProcessTaskGuid = Guid.NewGuid();
            newPT.ProcessGuid = processGuid;
            newPT.TaskName = "new task";
            newPT.TaskTypeId = pt.TaskTypeId;
            newPT.CreatedDate = DateTime.Now;
            newPT.CompletionTask = 1;
            newPT.DeletedDate = DateTime.Now;

            _context.ProcessTasks.Add(newPT);

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
            }

            
        }
    }
}
