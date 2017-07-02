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
    public class InstanceController : ApiController
    {
        private ApplicationDbContext _context;


        public InstanceController()
        {
            _context = new ApplicationDbContext();
        }

        //start a request with process id = id
        [Route("api/instance/{id}")]
        [HttpPut]
        public int CreateNewInstance(int id) {

            var process = _context.Processes
                .SingleOrDefault(c => c.Id == id);

            Instance instance = new Instance();
            instance.InstanceGuid = Guid.NewGuid();
            instance.StartDate = DateTime.Now;
            instance.InstanceState = "Started";
            instance.ProcessId = id;
            instance.LastMilestone = "Started";
            instance.ProcessGuid = process.ProcessGuid;
            instance.InstanceName = process.ProcessName;
            instance.ProcessName = process.ProcessName;

            _context.Instances.Add(instance);
            _context.SaveChanges();

            int instanceID = _context.Instances.Max(i => i.Id);

            return instanceID;
        }

        
        [Route("api/instances")]
        [HttpGet]
        public IEnumerable<Instance> GetInstances()
        {
            var Instances = _context.Instances;
            return Instances.ToList();
        }

        [Route("api/instances/{id}/details")]
        [HttpGet]
        public IEnumerable<InstanceTask> GetInstanceDetails(int id)
        {
            var iTasks =_context.InstanceTasks.Where(it => it.InstanceGuid == _context.Instances.Where(i => i.Id == id).FirstOrDefault().InstanceGuid)
                .ToList();

            return iTasks;
        }

        //process a request with request ID = id
        [Route("api/instance/processrequest/{id}")]
        [HttpPut]
        public int ProcessRequest(int id)
        {
            Instance instance = _context.Instances.Where(i => i.Id == id).FirstOrDefault();
            var ProcessTasks = _context.ProcessTasks.Where(pt => pt.ProcessGuid == instance.ProcessGuid).ToList();

            int processStarterID;
            int processEndID;

            foreach (var pt in ProcessTasks)
            {
                if (pt.TaskTypeId == 5)
                    processStarterID = pt.Id;

                if (pt.TaskTypeId == 6)
                    processEndID = pt.Id;
            }

            bool allTasksVisited = false;
            while (!allTasksVisited)
            {


                //iterate trough all the process tasks
                //if considered task has only one source task required, check if the source task is completed; if so execute the task


                allTasksVisited = true;

                foreach (var pt in ProcessTasks) {
                    //check if already existing in instanceTasks
                    bool alreadyCompletedTask = false;
                    var taskRecord = _context.InstanceTasks
                        .Where(it => it.ItaskGuid == pt.ProcessTaskGuid && it.InstanceGuid == instance.InstanceGuid)
                        .ToList();
                    if (taskRecord.Count > 0)
                    {
                        alreadyCompletedTask = true;
                        allTasksVisited = false;
                    }

                    if (!alreadyCompletedTask)
                    {
                        //pull all the source tasks that needs to be completed for the considered - destination task to start
                        var sourceTasks = _context.ProcessTaskTransition
                            .Where(ptt => ptt.DestinationTaskGuid == pt.ProcessTaskGuid)
                            .ToList();
                            

                        //if none is required, and it's the process starter
                        if (sourceTasks.Count == 0 && pt.TaskTypeId == 5) //if it's a process starter
                        {
                            executeTask(pt.TaskTypeId, instance.InstanceGuid, pt.ProcessTaskGuid, pt.TaskName);
                        }
                        //if considered task has at least 1 source tasks completions required,
                        //run a check if they all are in 'completed' 
                        //(check if all destination tasks have an existing instanceTask entry)
                        else if (sourceTasks.Count >= 1)
                        {
                            bool allRequiredSourceTasksCompleted = true;
                            
                            foreach(var st in sourceTasks)
                            {
                                var instanceTaskEntry = _context
                                    .InstanceTasks
                                    .Where(ite => ite.TaskGuid == st.SourceTaskGuid
                                   && ite.InstanceGuid == instance.InstanceGuid).ToList();
                                if (instanceTaskEntry.Count == 0) allRequiredSourceTasksCompleted = false;
                            }

                            if (allRequiredSourceTasksCompleted)
                            {
                                executeTask(pt.TaskTypeId, instance.InstanceGuid, pt.ProcessTaskGuid, pt.TaskName);
                            }
                        }
                    }
                }                
            }

            return id;
        }

        public void executeTask(int taskTypeID, Guid instanceGuid, Guid processTaskGuid, string taskName)
        {
            InstanceTask instanceTask = new InstanceTask();
            instanceTask.ItaskGuid = Guid.NewGuid();
            instanceTask.InstanceGuid = instanceGuid;
            instanceTask.TaskGuid = processTaskGuid;
            instanceTask.StartDate = DateTime.Now;
            instanceTask.TaskName = taskName;
            instanceTask.TaskTypeId = taskTypeID;


            Instance instance = _context.Instances
            .Where(i => i.InstanceGuid == instanceGuid)
            .FirstOrDefault();

            switch (taskTypeID)
            {
                case 5: //process starter                    
                    instanceTask.ItaskState = "Completed";
                    instanceTask.EndDate = DateTime.Now;                    
                    break;
                case 6: //process ender

                    //process end milestone settings
                    instanceTask.ItaskState = "Completed";
                    instanceTask.EndDate = DateTime.Now;
 
                    //request update
                    instance.LastMilestone = "Completed";
                    instance.InstanceState = "Completed";
                    instance.LastMilestoneDate = DateTime.Now;
                    instance.EndDate = DateTime.Now;
                    break;

                case 1: //milestone


                    var milestoneText = _context.ProcessTaskAttributes
                        .Where(pta => pta.ProcessTaskGuid == processTaskGuid && pta.AttributeKey == "milestone text")
                        .ToList();

                    if (milestoneText.Count > 0)
                    {
                        instance.LastMilestone = milestoneText[0].AttributeValue.ToString(); ;
                        instance.LastMilestoneDate = DateTime.Now;

                        instanceTask.ItaskState = "Completed";
                    }
                    else
                    {
                        instanceTask.ItaskState = "Failed";
                    }
                    instanceTask.EndDate = DateTime.Now;
                    break;

                case 2:

                    var PTAs = _context.ProcessTaskAttributes.Where(pta => pta.ProcessTaskGuid == processTaskGuid).ToList();

                    string recipientsList = string.Empty;
                    string bodyText = string.Empty;
                    string emailTitle = string.Empty;

                    foreach (var pta in PTAs)
                    {
                        if (pta.AttributeKey == "email address list")
                        {
                            recipientsList = pta.AttributeValue;
                        }
                        if (pta.AttributeKey == "email title")
                        {
                            emailTitle = pta.AttributeValue;
                        }
                        if (pta.AttributeKey == "email body")
                        {
                            bodyText = pta.AttributeValue;
                        }
                    }

                    if (recipientsList != string.Empty)
                    {
                        //sendEmail function
                        bool result = SendEmail(recipientsList, bodyText, emailTitle);

                        if (result)
                        {

                            instanceTask.ItaskState = "Completed";

                        }
                        else
                        {
                            instanceTask.ItaskState = "Failed";

                        }
                        instanceTask.EndDate = DateTime.Now;

                    }
                    else
                    {
                        instanceTask.ItaskState = "Failed";
                        instanceTask.EndDate = DateTime.Now;
                    }

                    break;
            }
            _context.InstanceTasks.Add(instanceTask);
            _context.SaveChanges();

         }


        public bool SendEmail(string recipientsList, string bodyText, string emailTitle)
        {

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("taskprocessor@gmail.com", "Taskprocessor123");

            MailMessage mm = new MailMessage("taskprocessor@gmail.com", recipientsList, emailTitle, bodyText);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            try
            {
                client.Send(mm);
                mm.Dispose();
                return true;
            }
            catch (Exception e)
            {
                mm.Dispose();
                return false;
            }
            
        }

    }
}