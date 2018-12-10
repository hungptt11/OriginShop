using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CML.Models.Model
{
    public class BaseObject
    {
        public DateTime? CreatedDate { get; set; }
        [MaxLength(30)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [MaxLength(30)]
        public string ModifiedBy { get; set; }
    }
}
