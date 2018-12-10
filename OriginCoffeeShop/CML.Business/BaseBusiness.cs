using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CML.Models.Model;
using System.Linq.Expressions;
using CML.Helper.Utilities;
using CML.Helper.DataContext;

namespace CML.Business
{
    public class BaseBusiness<T> : IBaseBusiness<T> where T : class, new()     
    {
        public bool Insert(T model)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.Insert(model);
            }
        }
        public bool Edit(T model)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.EditSingle(model);
            }
        }
        public bool EditRange(List<T> model)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.Edit(model);
            }
        }
        public bool InsertRange(List<T> models)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.InsertRange(models);
            }
        }
        public bool DeleteRange(List<T> models)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.DeleteAll(models);
            }
        }
        public bool Delete(T model)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.Delete(model);
            }
            
        }
        public T GetById(object id)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetById(id);
            }

        }
        public bool Delete(object id)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.Delete(id);
            }
        }
        public List<T> GetList()
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetAll();
            }
        }
        public List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.Get(predicate);
            }
        }
        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetItem(predicate);
            }
        }        
        public T GetSingleEntityByCommand(string sqlcommand)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetSingleEntityByCommand(sqlcommand);
            }
        }
        public List<T> GetListEntityByCommand(string sqlcommand)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetListEntityByCommand(sqlcommand);
            }
        }
        public List<T> GetListEntityBySP(string storedProcedureName, params object[] parameters)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetListEntityBySP(storedProcedureName, parameters);
            }
        }
        public async Task<int> ExecuteSqlCommandQuery(string sqlcommand)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return await repository.ExecuteSqlCommandQuery(sqlcommand);
            }
        }

        public T GetItem(string storeProcedure, params object[] paramater)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return repository.GetItem(storeProcedure, paramater);
            }
        }
        public async Task<int> ExecuteSqlCommandQuery(string sqlcommand, params object[] parameter)
        {
            using (Repository<T> repository = new Repository<T>())
            {
                return await repository.ExecuteSqlCommandQuery(sqlcommand, parameter);
            }
        }
       
    }
}
