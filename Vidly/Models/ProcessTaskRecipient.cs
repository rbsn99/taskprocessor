using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTaskRecipient
    {
        public int Id { get; set; }
        public Guid ProcessTaskRecipientGuid { get; set; }
        public Guid ProcessTaskGuid { get; set; }
        public int RecipientId { get; set; }
        public string RecipientType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}