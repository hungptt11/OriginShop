using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CML.Models.Model
{
    [Table("NewsCategory")]
    public class NewsCategory : BaseObject, IBaseOriginObj
    {
        [Key]
        public int Id { get; set; }
	    public string Name {get; set;}
	    public string MetaTitle {get; set;}
	    public int ParentID {get; set;}
	    public int DisplayOrder {get; set;}
	    public string SeoTitle {get; set;}
	    public string MetaKeywords {get; set;}
	    public string MetaDescriptions {get; set;}
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
    }
}
