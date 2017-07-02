using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class InstanceData
    {
        public int InstanceDataId { get; set; }
        public Guid InstanceDataGuid { get; set; }
        public Guid ItaskGuid { get; set; }
        public Guid TaskGuid { get; set; }
        public string DataLabel { get; set; }
        public string DataValue { get; set; }
    }
}