using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

namespace CML.Helper.Utilities
{
    public static class Extensions
    {
        public static readonly string[] AvailableDateFormat = { "dd/MM/yyyy", "dd/M/yyyy", "d/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "dd-M-yyyy", "d-MM-yyyy", "d-M-yyyy", "dd/MMM/yyyy", "d/MMM/yyyy", "MMM/dd/yyyy", "MMM/d/yyyy", "dd-MMM-yyyy", "d-MMM-yyyy", "MMM-dd-yyyy", "MMM-d-yyyy", "ddMMMyyyy", "dMMMyyyy", "MMMddyyyy", "MMMdyyyy", "ddMMyyyy", "ddMyyyy", "dMMyyyy", "dMyyyy" };
        public static bool IsNull<T>(this T obj) where T : class
        {
            if (obj == null) return true;
            else
            {
                if (typeof(T) == typeof(string))
                    return string.IsNullOrWhiteSpace(obj as string);
                else if (typeof(T) == typeof(DataSet))
                    return ((obj as DataSet).Tables.Count == 0);
                else if (typeof(T) == typeof(DataTable))
                    return ((obj as DataTable).Rows.Count == 0);
                else if (typeof(T) == typeof(Hashtable))
                    return ((obj as Hashtable).Count == 0);
                else if (typeof(T) == typeof(ArrayList))
                    return ((obj as ArrayList).Count == 0);
                else if (typeof(T) == typeof(Array))
                    return ((obj as Array).Length == 0);
                else
                    return false;
            }
        }
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            if (obj == null) return false;
            else
            {
                if (typeof(T) == typeof(string))
                    return !string.IsNullOrWhiteSpace(obj as string);
                else if (typeof(T) == typeof(DataSet))
                    return ((obj as DataSet).Tables.Count > 0);
                else if (typeof(T) == typeof(DataTable))
                    return ((obj as DataTable).Rows.Count > 0);
                else if (typeof(T) == typeof(Hashtable))
                    return ((obj as Hashtable).Count > 0);
                else if (typeof(T) == typeof(ArrayList))
                    return ((obj as ArrayList).Count > 0);
                else if (typeof(T) == typeof(Array))
                    return ((obj as Array).Length > 0);
                else
                    return true;
            }
        }
        public static bool IsNotContain(this string obj, string item)
        {
            if (obj.IsNull()) return false;
            else
            {
                if (obj.Contains(item))
                    return false;
                return true;
            }
        }
        public static bool IsNull<T>(this T[] obj)
        {
            return (obj == null || obj.Length == 0);
        }
        public static bool IsNull<T>(this List<T> obj)
        {
            return (obj == null || obj.Count == 0);
        }
        public static string ToUnSign(this string obj)
        {
            if (obj.IsNull()) return string.Empty;
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = obj.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static int CastToInt(this object obj)
        {
            if (obj == null) return -1;
            else
            {
                return (int)obj;
            }
        }
        public static byte ToByte(this object obj)
        {
            try
            {
                return obj.IsNull() ? (byte)0 : byte.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return (byte)0;
            }
        }
        public static decimal ToDecimal(this object obj)
        {
            try
            {
                return obj.IsNull() ? (decimal)0 : decimal.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return (decimal)0;
            }
        }
        public static double ToDouble(this object obj)
        {
            try
            {
                return obj.IsNull() ? 0.00 : double.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return 0.00;
            }
        }
        public static float ToFloat(this object obj)
        {
            try
            {
                return obj.IsNull() ? (float)0 : float.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return (float)0;
            }
        }
        public static int ToInt(this object obj)
        {
            try
            {
                return obj.IsNull() ? 0 : int.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static long ToLong(this object obj)
        {
            try
            {
                return obj.IsNull() ? (long)0 : long.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return (long)0;
            }
        }
        public static short ToShort(this object obj)
        {
            try
            {
                return obj.IsNull() ? (short)0 : short.Parse(obj.ToString().Trim().Replace(",", ""));
            }
            catch
            {
                return (short)0;
            }
        }
        public static double ToJulianDate(this DateTime obj)
        {
            try
            {
                return obj.ToOADate() + 2415018.5;
            }
            catch
            {
                return 0.0;
            }
        }
        public static DateTime ToDateTime(this double obj)
        {
            try
            {
                return DateTime.FromOADate(obj - 2415018.5);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ToDateTime(this string obj)
        {
            try
            {
                return obj.IsNull() ? DateTime.MinValue : DateTime.Parse(obj.Trim());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ToDateTime(this string obj, string format)
        {
            try
            {
                return obj.IsNull() ? DateTime.MinValue : DateTime.ParseExact(obj.Trim(), format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ToDateTime(this string obj, string[] format)
        {
            try
            {
                return obj.IsNull() ? DateTime.MinValue : format.IsNull() ? DateTime.ParseExact(obj.Trim(), AvailableDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None) : DateTime.ParseExact(obj.Trim(), format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static string[] Split(this string obj, string substr)
        {
            return (obj == null) ? new string[] { string.Empty } : obj.Split(new string[] { substr }, StringSplitOptions.None);
        }
    }
}
