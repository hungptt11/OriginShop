using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CML.Helper.DataContext
{
    public interface IRepository<T>
    {
        bool Insert(T entity);
        bool InsertRange(List<T> entitys);
        bool Delete(T entity);
        List<T> Get(Expression<Func<T, bool>> predicate);
        List<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending, int skip, int take);
        List<T> Get(Expression<Func<T, bool>> predicate, string groupBy, string fieldOrderBy, bool ascending, int take);
        List<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending);
        List<T> GetAll();
        T GetById(object id);
        List<T> Get(string query, params object[] parameters);
        List<SqlParameter> GetOutPut(string storedProcedureName, params object[] parameters);
    }
}
