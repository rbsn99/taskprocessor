using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Instance
    {
        public int Id { get; set; }
        public Guid InstanceGuid { get; set; }
        public string InstanceName { get; set; }
        public string InstanceState { get; set; }
        public string LastMilestone { get; set; }
        public DateTime? LastMilestoneDate { get; set; }
        public int? RequesterId { get; set; }
        public int? ProcessId { get; set; }
        public Guid? ProcessGuid { get; set; }
        public string ProcessName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
 
    }
}