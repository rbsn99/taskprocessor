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

        [Route("api/processes/{processTitle}")]
        [HttpPut]
        public int CreateProcess(string processTitle)
        {
            Process process = new Process();
            process.ProcessName = processTitle;
            process.ProcessGuid = Guid.NewGuid();
            process.CreatedDate = DateTime.Now;
            process.DeletedDate = null;


            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Processes.Add(process);
            _context.SaveChanges();

            int processID = _context.Processes.Max(p => p.Id);

            return processID;
        }

        [Route("api/processes/initialize/{id}")]
        [HttpPut]
        public int InitializeProcess(int id)
        {
            Guid processGuid = _context.Processes.Where(p => p.Id == id).FirstOrDefault().ProcessGuid;

            ProcessTask ptStarter = new ProcessTask();
            ptStarter.ProcessTaskGuid = Guid.NewGuid();
            ptStarter.ProcessGuid = processGuid;
            ptStarter.TaskName = "Process Start";
            ptStarter.TaskTypeId = 5;
            ptStarter.CreatedDate = DateTime.Now;
            ptStarter.CompletionTask = 1;
            ptStarter.DeletedDate = null;

            ProcessTask ptEnder = new ProcessTask();
            ptEnder.ProcessTaskGuid = Guid.NewGuid();
            ptEnder.ProcessGuid = processGuid;
            ptEnder.TaskName = "Process End";
            ptEnder.TaskTypeId = 6;
            ptEnder.CreatedDate = DateTime.Now;
            ptEnder.CompletionTask = 1;
            ptEnder.DeletedDate = null;

            _context.ProcessTasks.Add(ptStarter);
            _context.ProcessTasks.Add(ptEnder);

            _context.SaveChanges();

            return id;
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


     
        [Route("api/processes/tree/{id}")]
        [HttpGet]
        public List<ProcessTaskTree> GetProcessTree(int id)
        {
            Guid processGuid = _context.Processes.SingleOrDefault(c => c.Id == id).ProcessGuid;

            var processTasks = _context.ProcessTasks.Where(pt => pt.ProcessGuid == processGuid).ToList();

            bool allNodesAdded = true;
            List<ProcessTaskTree> ptree = new List<ProcessTaskTree>();
            ProcessTaskTree root = new ProcessTaskTree();

            foreach (var pt in processTasks)
            {
                if (pt.TaskTypeId == 5)
                {
                    root.name = pt.TaskName;
                    root = updateTree(root, pt.ProcessTaskGuid);
                }
            }

            ptree.Add(root);
            return ptree;
        }

        ProcessTaskTree updateTree(ProcessTaskTree node, Guid nodeGuid)
        {
            var children = _context.ProcessTaskTransition.Where(ptt => ptt.SourceTaskGuid == nodeGuid).ToList();

            if (children.Count == 0) return node;

            else
            {

                node.children = new List<ProcessTaskTree>();

                foreach (var child in children)
                {
                    ProcessTaskTree newNode = new ProcessTaskTree();

                    var destinationTask = _context.ProcessTasks
                        .Where(cd => cd.ProcessTaskGuid == child.DestinationTaskGuid)
                        .FirstOrDefault();

                    newNode.name = destinationTask.TaskName;

                    node.children.Add(newNode);

                    var destChildren = _context.ProcessTaskTransition.Where(ptt => ptt.SourceTaskGuid == destinationTask.ProcessTaskGuid)
                        .ToList();
                    if (destChildren.Count > 0)
                    {
                        newNode = updateTree(newNode, destinationTask.ProcessTaskGuid);
                    }

                }

                return node; 
            }
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
        public IEnumerable<V_PT>  GetProcessTasks(int id)
        {
            var process = _context.Processes.SingleOrDefault(c => c.Id == id);

            List<ProcessTask> processTasks = _context.ProcessTasks.Where(c => c.ProcessGuid == process.ProcessGuid).ToList();

            List<V_PT> PTview = new List<V_PT>();

             foreach (ProcessTask pt in processTasks)
            {
                V_PT ptv = new V_PT();
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
                if (dep != string.Empty) {

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

            /* ************************ UPDATE ProcessTaskAttributes DATA ************************ */
 
            //split the attributes along with their values
            List<string> attrGiven = ptu.ProcessTaskAttributes.Trim().Split(';').ToList();

            
            foreach (var attr in attrGiven)
            {
                //split into key - value pair
                List<string> attributesVSvalues = attr.Trim().Split('=').ToList();


                //if there was one found
                if (attributesVSvalues.Count > 0 && attr != "")
                {
                    //find a record with the specific key

                    string keyToCompare = attributesVSvalues[0].ToString();

                    var PTAttribute = _context.ProcessTaskAttributes
                     .Where(pta => pta.ProcessTaskGuid == ptu.ProcessTaskGuid 
                     && pta.AttributeKey == keyToCompare)
                     .ToList();

                    //if one found, let's check if need to update it's value
                    if (PTAttribute.Count > 0)
                    {
                        PTAttribute[0].AttributeValue = attributesVSvalues[1];
                        _context.SaveChanges();

                    }
                    //else let's create one
                    else
                    {
                        ProcessTaskAttribute newPTA = new ProcessTaskAttribute();

                        newPTA.AttributeKey = attributesVSvalues[0];
                        newPTA.AttributeValue = attributesVSvalues[1];
                        newPTA.ProcessTaskGuid = ptu.ProcessTaskGuid;
                        newPTA.CreatedDate = DateTime.Now;
                        newPTA.ProcessTaskAttributeGuid = Guid.NewGuid();

                        _context.ProcessTaskAttributes.Add(newPTA);
                        _context.SaveChanges();
                    }
                }
            }
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
            newPT.TaskName = "(new task)";
            newPT.TaskTypeId = pt.TaskTypeId;
            newPT.CreatedDate = DateTime.Now;
            newPT.CompletionTask = 1;

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
