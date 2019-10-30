using System;
using System.Data;

namespace WLib.Core.Bll.Extensions
{
    public static class DataReaderExtensions
    {

        public static int ToInt(this IDataReader reader, string propName, int defaultValue = 0)
        {
            if (HasColumn(reader, propName))
            {
                return reader[propName]?.ToString()?.ToInt(defaultValue) ?? defaultValue;
            }

            return defaultValue;
        }



        public static decimal ToDecimal(this IDataReader reader, string propName, decimal defaultValue = 0)
        {
            if (HasColumn(reader, propName))
            {
                return reader[propName]?.ToString()?.ToDecimal(defaultValue) ?? defaultValue;
            }

            return defaultValue;
        }

        public static bool ToBool(this IDataReader reader, string propName, bool defaultValue = false)
        {
            if (HasColumn(reader, propName))
            {
                return reader[propName]?.ToString()?.ToBool(defaultValue) ?? defaultValue;
            }

            return defaultValue;
        }

        public static DateTime? ToDate(this IDataReader reader, string propName, DateTime? defaultValue = null)
        {
            if (HasColumn(reader, propName))
            {
                return reader[propName]?.ToString()?.ToDateTime(defaultValue) ?? defaultValue;
            }

            return defaultValue;
        }

        public static string ToStringValue(this IDataReader reader, string propName, string defaultValue = "")
        {
            if (HasColumn(reader, propName))
            {
                return reader[propName]?.ToString() ?? defaultValue;
            }

            return defaultValue;
        }

        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }


        //public IEnumerable<S> Read3<S>(string query, Func<IDataRecord, S> selector)
        //{
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandText = query;
        //        cmd.Connection.Open();
        //        using (var r = cmd.ExecuteReader())
        //            while (r.Read())
        //                yield return selector(r);
        //    }
        //}

    }
}