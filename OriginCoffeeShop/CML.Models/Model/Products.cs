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
    [Table("Products")]
    public class Products : BaseObject
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [MaxLength(200, ErrorMessage = ConstantMessage.MaxLengthErrors)]
        public string ProductName {get; set;}
        [Required]
        public string MetaTitle {get; set;}
        [AllowHtml]
        [Required]
        public string Description {get; set;}
        [Required]
        public string ProductImage {get; set;}
        [Required]
        public string MoreImages {get; set;}
        [MaxLength(40)]
        public string Price {get; set;}
        public string PromotionPrice {get; set;}
        public bool   IncludeVAT {get; set;}
        public int CategoryID {get; set;}
        [AllowHtml]
        [Required]
        public string Detail {get; set;}
        public bool Status {get; set;}
        public bool TopHot {get; set;}
        public long ViewCounts { get; set; }
    }
}
