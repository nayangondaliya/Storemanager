using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static async Task<List<T>> MapToList<T>(this SqlDataReader dr)
        {
            string currentPropName = string.Empty;

            try
            {
                List<T> list = new List<T>();
                T obj = default(T);

                while (await dr.ReadAsync())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        //  Check has column in case we have extension property
                        if (!dr.HasColumn(prop.Name)) continue;

                        currentPropName = prop.Name;
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            //  Need to check object type boolean here because return result from db type will be ulong64 which can't convert to bool
                            if (IsBoolean(prop))
                            {
                                prop.SetValue(obj, Convert.ToBoolean(dr[prop.Name]), null);
                            }
                            else
                            {
                                prop.SetValue(obj, dr[prop.Name], null);
                            }
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while converting object '{currentPropName}' from data reader to obj class ", ex);
            }
        }

        public static async Task<T> Map<T>(this SqlDataReader dr)
        {
            return (await dr.MapToList<T>()).FirstOrDefault();
        }

        public static async Task<int> GetInt(this SqlDataReader dr)
        {
            while (await dr.ReadAsync())
            {
                return dr.GetInt32(0);
            }

            //  TODO: Do we need to throw exception?
            return 0;
        }

        public static bool HasColumn(this SqlDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase)) return true;
            }

            return false;
        }

        private static bool IsBoolean(PropertyInfo prop)
        {
            return prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?);
        }
    }
}