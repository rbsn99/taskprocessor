using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ProcessTaskType
    {
        public int Id { get; set; }
        public string ProcessTaskTypeName { get; set; }
        public string AttributeKeyName { get; set; } //e.g. Approval decisions list, form ID, milestone name, e-mail address, e-mail title
        public DateTime CreatedDate { get; set; }
    }
}