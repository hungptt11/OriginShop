using System;
using System.Collections.Generic;
using System.Text;
using CML.NetCore.Repository;
using CML.NetCore.Models.Models;
using Microsoft.Extensions.Logging;

namespace CML.NetCore.Services
{
    public class BaseServices<T> : IDisposable, IBaseServices<T> where T : class, new()
    {
        Repository<T> _repository;
        public BaseServices(DatabaseDatacontex context, ILogger<T> logger)
        {
            _repository = new Repository<T>(context, logger);
        }
        public bool Insert(T model)
        {
            return _repository.Insert(model);
        }
        public bool Edit(T model)
        {
            return _repository.EditSingle(model);
        }
        public bool EditRange(List<T> model)
        {
            return _repository.Edit(model);
        }
        public bool InsertRange(List<T> models)
        {
            return _repository.InsertRange(models);
        }
        public bool DeleteRange(List<T> models)
        {
            return _repository.DeleteAll(models);
        }
        public bool Delete(T model)
        {
            return _repository.Delete(model);
        }
        public T GetById(object id)
        {
            return _repository.GetById(id);
        }
        public bool Delete(object id)
        {
            return _repository.Delete(id);
        }
        public List<T> GetList()
        {            
            return _repository.GetAll();
        }
        public bool ExcuteSP(string storeProcedure, params object[] paramater)
        {            
            return _repository.ExcuteSP(storeProcedure, paramater);
        }
        public T GetSingleEntityByCommand(string sqlcommand)
        {            
            return _repository.GetSingleEntityByCommand(sqlcommand);
        }
        public List<T> GetListEntityByCommand(string sqlcommand)
        {            
            return _repository.GetListEntityByCommand(sqlcommand);
        }
        public List<T> GetListEntityBySP(string storedProcedureName, params object[] parameters)
        {
            return _repository.GetListEntityBySP(storedProcedureName, parameters);
        }
        public int ExecuteSqlCommandQuery(string sqlcommand)
        {
            return _repository.ExecuteSqlCommandQuery(sqlcommand);
        }

        public T GetItem(string storeProcedure, params object[] paramater)
        {
            return _repository.GetItem(storeProcedure, paramater);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
