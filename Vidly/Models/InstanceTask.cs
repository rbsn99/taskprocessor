using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class InstanceTask
    {
        public int Id { get; set; }
        public Guid ItaskGuid { get; set; }
        public Guid InstanceGuid { get; set; }
        public string ItaskState { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid TaskGuid { get; set; }
        public int? TaskTypeId { get; set; }
        public string TaskName { get; set; }
    }
}