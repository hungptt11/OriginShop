using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CML.Models.Model
{
    [Table("UserToken")]
    public class UserToken
    {
        public UserToken () {
            this.Token = 9999;
        }   
        [Key]
        public string UserName { get; set; }
        public int Token { get; set; }
    }
}
