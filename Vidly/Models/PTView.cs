using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class PTView
    {
        public int Id { get; set; }
        public Guid ProcessTaskGuid { get; set; }
        public Guid ProcessGuid { get; set; }
        public Process Process { get; set; }
        public int TaskTypeId { get; set; }
        public ProcessTaskType ProcessTaskType { get; set; }
        public string TaskName { get; set; }
        public byte CompletionTask { get; set; }
        public ProcessTaskRecipient ProcessTaskRecipient { get; set; }
        public List<ProcessTaskAttribute> ProcessTaskAttributes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}