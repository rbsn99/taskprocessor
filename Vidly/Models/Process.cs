using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Process
    {
        public int Id { get; set; }
        public Guid ProcessGuid { get; set; }
        public string ProcessName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}