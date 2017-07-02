using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTask
    {
        public int Id { get; set; }
        public Guid ProcessTaskGuid { get; set; }
        public Guid ProcessGuid { get; set; }
        public int TaskTypeId { get; set; }
        public string TaskName { get; set; }
        public byte CompletionTask { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}