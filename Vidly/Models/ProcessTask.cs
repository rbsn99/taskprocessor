using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTask
    {
        public int Id { get; set; }
        public Guid ProcessGuid { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}