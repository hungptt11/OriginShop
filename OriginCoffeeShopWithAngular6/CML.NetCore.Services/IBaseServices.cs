using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.NetCore.Services
{
    public interface IBaseServices<T> where T : class, new()
    {
        bool Insert(T model);
        bool Edit(T model);
        bool Delete(T model);
        T GetById(object id);
        bool Delete(object id);
        List<T> GetList();
    }
}
