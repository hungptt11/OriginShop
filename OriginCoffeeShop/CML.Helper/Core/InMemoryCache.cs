using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading;

namespace CML.Helper.Core
{
    public class InMemoryCache : ICacheService
    {
        private MemoryCache mem = new MemoryCache("hungpt11");
        public T GetItem<T>(string cacheKey) where T : class
        {
            try
            {
                var item = mem.Get(cacheKey) as T;
                return item;
            }
            catch
            {
                return null;
            }
        }
        public List<T> GetListItem<T>(string cacheKey) where T : class
        {
            try
            {
                var item = mem.Get(cacheKey) as List<T>;
                return item;
            }
            catch
            {
                return null;
            }
        }
        public void Remove(string cacheKey)
        {
            mem.Remove(cacheKey);
        }
        public bool ContainKey(string cacheKey)
        {
            return mem.Contains(cacheKey);
        }
        public void Add(string cacheKey, object data, int minutes = 180)
        {
            try
            {
                mem.Add(cacheKey, data, DateTime.Now.AddMinutes(minutes));
            }
            catch
            {

            }
        }
        public void ResetCache()
        {
            mem.Dispose();
            mem = new MemoryCache("hungpt11");
        }
    }

    interface ICacheService
    {
        T GetItem<T>(string cacheKey) where T : class;
        List<T> GetListItem<T>(string cacheKey) where T : class;
        void Remove(string cacheKey);
    }
    public sealed class GlobalCache
    {
       private static InMemoryCache instance = new InMemoryCache();
       private static object syncRoot = new Object();
       public static InMemoryCache Instance
       {
           get
           {
               if (instance == null)
               {
                   lock (syncRoot)
                   {
                       if (instance == null)
                           instance = new InMemoryCache();
                   }
               }

               return instance;
           }           
       }     
       public static bool ResetCache()
       {
           try
           {
               if (instance != null)
               {
                   instance.ResetCache();
                   instance = null;
               }
           } catch
           {
               return false;
           }
           return true;
       }
    }
}
