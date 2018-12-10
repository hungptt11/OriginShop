using System;
using System.Collections.Generic;
using System.Text;
using CML.NetCore.Repository;
using CML.NetCore.Models.Models;
using Microsoft.Extensions.Logging;

namespace CML.NetCore.Services.DanhMuc
{
    public interface IProductCategoryService<T> : IBaseServices<T> where T : class, new ()
    {
        List<T> GetListEntityBySP(string storedProcedureName, params object[] parameters);
    }
    public class ProductCategoryService<T> : BaseServices<T>, IProductCategoryService<T> where T : class, new()
    {
        public ProductCategoryService(DatabaseDatacontex context, ILogger<T> logger) : base(context, logger)
        {
            
        }
    }
}
