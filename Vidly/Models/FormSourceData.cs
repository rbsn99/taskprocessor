﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class FormSourceData
    {
        public int Id { get; set; }
        public Guid FormSourceDataGuid { get; set; }
        public string FormName { get; set; }
        public string FormDescription { get; set; }
        public string FormSourceCode { get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}