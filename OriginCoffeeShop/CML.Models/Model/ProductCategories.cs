using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CML.Models.Model
{
    [Table("ProductCategories")]
    public class ProductCategories : BaseObject
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [MaxLength(200,ErrorMessage= ConstantMessage.MaxLengthErrors)]
        public string Name {get; set;}
        [Required]
        public string MetaTitle {get; set;}
        public int ParentID {get; set;}
        public int DisplayOrder {get; set;}       
        public bool Status  {get; set;}
        public bool ShowOnHome {get; set;} 
    }
}
