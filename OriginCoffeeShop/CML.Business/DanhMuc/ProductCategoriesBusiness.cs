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
    public interface IProductCategoriesBusiness<T> : IBaseBusiness<T> where T : class, new()     
    {
    }
    public class ProductCategoriesBusiness<T> : BaseBusiness<T>, IProductCategoriesBusiness<T> where T : class, new()     
    {
        public ProductCategoriesBusiness()
        {
            
        }
    }
}
