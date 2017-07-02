using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTaskAttribute
    {
        public int Id { get; set; }
        public Guid ProcessTaskAttributeGuid { get; set; }
        public Guid ProcessTaskGuid { get; set; }
        public string AttributeKey { get; set; }
        public string AttributeValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}