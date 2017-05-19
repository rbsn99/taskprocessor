using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTaskTransition
    {
        public int ProcessTaskTransitionId { get; set; }
        public Guid ProcessTaskTransitionGuid { get; set; }
        public Guid SourceTaskGuid { get; set; }
        public Guid DestinationTaskGuid { get; set; }
        public string SourceTaskState { get; set; } //rule made on source task state, e.g. Failed, Completed, In progress
        public string TransitionType { get; set; } //initiating or cancellation
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}