using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Models
{
    public class ConstantMessage
    {
        public const string NotNullErrors = "{0} không được rỗng";
        public const string MaxLengthErrors = "{0} Bắt buộc nhập nhập tối thiểu";
        public const string MinLengthErrors = "{0} Chỉ được nhập tối đa";
        public const string AnphanbetOnly = "{0} không được chứa ký tự đặc biệt và số";
        public const string AnphanumbericOnly = "{0} không được chứa ký tự đặc biệt";
        public const string Phonenumber = "{0} Không phải là số điện thoại";
        
    }
}
