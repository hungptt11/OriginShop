using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CML.Helper.DataContext;
using CML.Models.Model;
using System.Linq.Expressions;
using CML.Helper.Utilities;

namespace CML.Business
{
    public interface IProductsBusiness<T> : IBaseBusiness<T> where T : class, new()     
    {
    }
    public class ProductsBusiness<T> : BaseBusiness<T>, IProductsBusiness<T> where T : class, new()     
    {
        public ProductsBusiness()
        {
            
        }
    }
}
