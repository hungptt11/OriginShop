using System;
using System.Collections.Generic;
using System.Text;

namespace CML.NetCore.Models.Models
{
    public class BaseClass
    {
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
