using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class PTUpdate
    {
        public int Id { get; set; }
        public Guid ProcessTaskGuid { get; set; }
        public string ProcessTaskTypeName { get; set; }
        public string TaskName { get; set; }
        public string ProcessTaskRecipient { get; set; }
        public string ProcessTaskAttributes { get; set; }
    }
}