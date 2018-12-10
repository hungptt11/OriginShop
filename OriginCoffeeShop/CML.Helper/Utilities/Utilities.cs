using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CML.Helper.Core;

namespace CML.Helper.Utilities
{
    public class Utility
    {
        public static object[] ConvertObjectToArrayParam<T>(T data) where T : class, new()
        {
            try
            {
                var props = TypeDescriptor.GetProperties(typeof(T));
                object[] values = new object[props.Count];
                for (int i = 0; i < props.Count; i++)
                {
                    values[i] = props[i].GetValue(data);
                }
                return values;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return null;
            }
        }
    }
}
