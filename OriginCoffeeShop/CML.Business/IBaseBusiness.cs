using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CML.Business
{
    public interface IBaseBusiness<T>  where T : class, new()
    {
        bool Insert(T model) ;
        bool Edit(T model);
        bool Delete(T model);
        T GetById(object id);
        bool Delete(object id);
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> predicate);
        T GetItem(Expression<Func<T, bool>> predicate);
        Task<int> ExecuteSqlCommandQuery(string sqlcommand, params object[] parameter);
    }
}
