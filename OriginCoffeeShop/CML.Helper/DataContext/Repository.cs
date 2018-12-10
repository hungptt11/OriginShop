using CML.Helper.Core;
using CML.Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CML.Helper.Utilities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CML.Helper.DataContext
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        readonly DatabaseDatacontex  _dataContext;
        public Repository()
        {
            _dataContext = new DatabaseDatacontex();            
        }
        public void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
        #region IRepository<T> Members

        /// <summary>
        /// Insert entity in DataBase
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Boolean</returns>
        public bool Insert(T entity)
        {
            try
            {                
                _dataContext.Set<T>().Add(entity);
                return _dataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        public bool InsertRange(List<T> entitys)
        {
            try
            {
                _dataContext.Set<T>().AddRange(entitys);
                return _dataContext.SaveChanges() > 0;                
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        public bool Edit(List<T> entitys)
        {
            try
            {
                foreach (T e in entitys)
                {
                    dynamic model = e;
                    var entityFind = _dataContext.Set<T>().Find(model.Id);

                    if (entityFind != null)
                    {
                        var entry = _dataContext.Entry(entityFind);
                        entry.CurrentValues.SetValues(e);
                    }                    
                }

                return _dataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        public bool EditSingle(T entity)
        {
            try
            {
                    dynamic model = entity;
                    var entityFind = _dataContext.Set<T>().Find(model.Id);

                    if (entityFind != null)
                    {
                        var entry = _dataContext.Entry(entityFind);
                        entry.CurrentValues.SetValues(entity);
                    }

                return _dataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// Delete entity in DataBase
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Boolean</returns>
        public bool Delete(T entity)
        {
            try
            {
                _dataContext.Set<T>().Remove(entity);
                return _dataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }        

        /// <summary>
        /// Delete model in DataBase
        /// </summary>
        /// <param name="id">Key in entity</param>
        /// <returns>Boolean</returns>
        public bool Delete(object id)
        {
            try
            {
                T entity = GetById(id);
                if (entity != null)
                {
                    _dataContext.Set<T>().Remove(entity);
                    return _dataContext.SaveChanges() > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// Delete multiple records
        /// </summary>
        /// <param name="list">list entity</param>
        /// <returns>Boolean</returns>
        public bool DeleteAll(List<T> list)
        {
            try
            {
                var db = _dataContext.Set<T>();
                foreach (var item in list)
                {
                    db.Attach(item);
                }
                db.RemoveRange(list);                
                return _dataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// Get list entity by condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>list entity</returns>
        public List<T> Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dataContext.Set<T>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        /// <summary>
        /// Get list entity by condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>list entity</returns>
        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dataContext.Set<T>().SingleOrDefault(predicate);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get list entity by condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <param name="fieldOrderBy">field order by</param>
        /// <param name="ascending">sort</param>
        /// <returns>list entity</returns>
        public List<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending)
        {
            try
            {
                var p = typeof(T).GetProperty(fieldOrderBy);
                var t = p.PropertyType;
                if (t == typeof(int))
                {
                    var pe = Expression.Parameter(typeof(T), "p");
                    var expr1 = Expression.Lambda<Func<T, int>>(Expression.Property(pe, fieldOrderBy), pe);
                    return (ascending
                        ? _dataContext.Set<T>().Where(predicate).OrderBy(expr1).ToList()
                        : _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).ToList());
                }
                else
                {
                    if (t == typeof(int?))
                    {
                        var pe = Expression.Parameter(typeof(T), "p");
                        var expr1 = Expression.Lambda<Func<T, int?>>(Expression.Property(pe, fieldOrderBy), pe);
                        return (ascending
                            ? _dataContext.Set<T>().Where(predicate).OrderBy(expr1).ToList()
                            : _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).ToList());
                    }
                    else
                    {
                        var pe = Expression.Parameter(typeof(T), "p");
                        var expr1 = Expression.Lambda<Func<T, String>>(Expression.Property(pe, fieldOrderBy), pe);
                        return (ascending
                            ? _dataContext.Set<T>().Where(predicate).OrderBy(expr1).ToList()
                            : _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).ToList());
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get list entity by condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <param name="fieldOrderBy">field order by</param>
        /// <param name="ascending">sort</param>
        /// <param name="skip">skip</param>
        /// <param name="take">take</param>
        /// <returns>list entity</returns>
        public List<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending, int skip, int take)
        {
            try
            {
                var p = typeof(T).GetProperty(fieldOrderBy);
                var t = p.PropertyType;
                if (t == typeof(int))
                {
                    var pe = Expression.Parameter(typeof(T), "p");
                    var expr1 = Expression.Lambda<Func<T, int>>(Expression.Property(pe, fieldOrderBy), pe);
                    return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(expr1).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).Skip(skip).Take(take).ToList());
                }
                else
                 if (t == typeof(DateTime))
                {
                    var pe = Expression.Parameter(typeof(T), "p");
                    var expr1 = Expression.Lambda<Func<T, DateTime>>(Expression.Property(pe, fieldOrderBy), pe);
                    return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(expr1).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).Skip(skip).Take(take).ToList());
                }
                else
                {
                    var pe = Expression.Parameter(typeof(T), "p");
                    var expr1 = Expression.Lambda<Func<T, string>>(Expression.Property(pe, fieldOrderBy), pe);
                    return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(expr1).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        ///  Get list entity by condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <param name="groupBy">group by</param>
        /// <param name="fieldOrderBy">field order by</param>
        /// <param name="ascending">sort</param>
        /// <param name="take">take</param>
        /// <returns>list entity by condition</returns>
        public List<T> Get(Expression<Func<T, bool>> predicate, string groupBy, string fieldOrderBy, bool ascending, int take)
        {
            try
            {
                var p = typeof(T).GetProperty(fieldOrderBy);
                var t = p.PropertyType;
                if (t == typeof(int))
                {
                    var pe = Expression.Parameter(typeof(T), "p");
                    var expr1 = Expression.Lambda<Func<T, int>>(Expression.Property(pe, fieldOrderBy), pe);

                    var fieldXExpression = Expression.Property(pe, groupBy);
                    var lambda = Expression.Lambda<Func<T, object>>(
                        fieldXExpression,
                        pe);
                    if (ascending)
                    {
                        var data = _dataContext.Set<T>().Where(predicate).OrderBy(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                        var list = new List<T>();
                        foreach (var item in data)
                        {
                            list.AddRange(item);
                        }
                        return list;
                    }
                    else
                    {
                        var data = _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                        var list = new List<T>();
                        foreach (var item in data)
                        {
                            list.AddRange(item);
                        }
                        return list;
                    }
                }
                else
                {
                    var pe = Expression.Parameter(typeof(T), "p");
                    var expr1 = Expression.Lambda<Func<T, string>>(Expression.Property(pe, fieldOrderBy), pe);
                    var fieldXExpression = Expression.Property(pe, groupBy);
                    var lambda = Expression.Lambda<Func<T, object>>(
                       fieldXExpression,
                        pe);
                    if (ascending)
                    {
                        var data = _dataContext.Set<T>().Where(predicate).OrderBy(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                        var list = new List<T>();
                        foreach (var item in data)
                        {
                            list.AddRange(item);
                        }
                        return list;
                    }
                    else
                    {
                        var data = _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                        var list = new List<T>();
                        foreach (var item in data)
                        {
                            list.AddRange(item);
                        }
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get count list entity by condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>count list entity by condition </returns>
        public int GetCount(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate != null)
                    return _dataContext.Set<T>().Where(predicate).Count();
                else
                    return _dataContext.Set<T>().Count();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return -1;
            }
        }
        public int GetCount(string SQLCOMAND)
        {
            try
            {
                return _dataContext.Database.SqlQuery<int>(SQLCOMAND).ToInt();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return -1;
            }
        }

        /// <summary>
        /// Get all entity
        /// </summary>
        /// <returns>list entity</returns>
        public List<T> GetAll()
        {
            try
            {
                return _dataContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="id">Key in entity</param>
        /// <returns>entity</returns>
        public T GetById(object id)
        {
            try
            {
                return _dataContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get list entity
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters">parameters input</param>
        /// <returns>list entity</returns>
        public List<T> Get(string storedProcedureName, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    var query = string.Concat("Exec ", storedProcedureName, " ");

                    foreach (var item in parameters)
                    {
                        var itemObject = (SqlParameter)item;
                        query += string.Concat(itemObject.ParameterName, ",");
                    }
                    query = parameters.Length > 0 ? query.Substring(0, query.Length - 1) : storedProcedureName;

                    return _dataContext.Database.SqlQuery<T>(query, parameters).ToList();
                }
                else
                {
                    return _dataContext.Database.SqlQuery<T>(storedProcedureName).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        /// <summary>
        /// Exceute store procedure
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters">parameters input</param>
        /// <returns>list entity</returns>
        public bool ExcuteStoreProcdureByExecuteSqlCommand(string storedProcedureName, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    var query = string.Concat("Exec ", storedProcedureName, " ");

                    foreach (var item in parameters)
                    {
                        var itemObject = (SqlParameter)item;
                        query += string.Concat(itemObject.ParameterName, ",");               
                    }
                    query = parameters.Length > 0 ? query.Substring(0, query.Length - 1) : storedProcedureName;
                    return _dataContext.Database.ExecuteSqlCommand(query, parameters) > 0;                   
                }
                else
                {
                    return _dataContext.Database.ExecuteSqlCommand(storedProcedureName) > 0;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        /// <summary>
        /// Get a single Entity by command
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters">parameters input</param>
        /// <returns>list entity</returns>
        public T GetSingleEntityByCommand(string sqlcommand)
        {
            try
            {
                return _dataContext.Database.SqlQuery<T>(sqlcommand).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get List Entity by command
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters">parameters input</param>
        /// <returns>list entity</returns>
        public  List<T> GetListEntityByCommand(string sqlcommand)
        {
            try
            {

                return _dataContext.Database.SqlQuery<T>(sqlcommand).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        /// <summary>
        /// Get List Entity by storeprocedule
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters">parameters input</param>
        /// <returns>list entity</returns>
        public List<T> GetListEntityBySP(string storedProcedureName, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    var query = string.Concat("Exec ", storedProcedureName, " ");

                    foreach (var item in parameters)
                    {
                        var itemObject = (SqlParameter)item;
                        query += string.Concat(itemObject.ParameterName, ",");
                    }
                    query = parameters.Length > 0 ? query.Substring(0, query.Length - 1) : storedProcedureName;
                    return _dataContext.Database.SqlQuery<T>(query, parameters).ToList();         
                } else
                {
                    return _dataContext.Database.SqlQuery<T>(storedProcedureName).ToList();
                }
                
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Get a single Entity by command
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters">parameters input</param>
        /// <returns>list entity</returns>
        public async Task<int> ExecuteSqlCommandQuery(string sqlcommand,params object[] parameter)
        {
            try
            {
                return await _dataContext.Database.ExecuteSqlCommandAsync(sqlcommand, parameter);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return -1;
            }
        }
        

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="storedProcedureName">stored procedure name</param>
        /// <param name="parameters"></param>
        /// <returns>entity</returns>
        public T GetItem(string storedProcedureName, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    var query = string.Concat("Exec ", storedProcedureName, " ");

                    foreach (var item in parameters)
                    {
                        var itemObject = (SqlParameter)item;
                        query += string.Concat(itemObject.ParameterName, ",");
                    }
                    query = parameters.Length > 0 ? query.Substring(0, query.Length - 1) : storedProcedureName;
                    return _dataContext.Database.SqlQuery<T>(query, parameters).FirstOrDefault();  
                }
                else
                {
                    return _dataContext.Database.SqlQuery<T>(storedProcedureName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }

        public List<SqlParameter> GetOutPut(string storedProcedureName, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    var query = string.Concat("", storedProcedureName, " ");

                    var listParameterOutPut = new List<SqlParameter>();

                    foreach (var item in parameters)
                    {
                        var itemObject = (SqlParameter)item;

                        if (itemObject.Direction == ParameterDirection.Output)
                        {
                            listParameterOutPut.Add(itemObject);
                            query += string.Concat(itemObject.ParameterName, " OUT,");
                        }
                        else
                        {
                            query += string.Concat(itemObject.ParameterName, ",");
                        }
                    }
                    query = parameters.Length > 0 ? query.Substring(0, query.Length - 1) : storedProcedureName;

                    _dataContext.Database.ExecuteSqlCommand(query, parameters);                    
                    return listParameterOutPut;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
        #endregion
        
    }
}
