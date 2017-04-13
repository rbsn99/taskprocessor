using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTaskAttribute
    {
        public int Id { get; set; }
        public int ProcessTaskTypeId { get; set; }
        public string AttributeKey { get; set; }
        public string AttributeValue { get; set; }
    }
}