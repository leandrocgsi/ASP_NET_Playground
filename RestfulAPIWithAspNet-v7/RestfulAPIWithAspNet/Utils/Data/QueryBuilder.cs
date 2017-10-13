﻿using RestfulAPIWithAspNet.Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Reflection;

namespace RestfulAPIWithAspNet.Utils.Data
{
    //SEE: https://msdn.microsoft.com/en-us/library/bb425822.aspx
    public class QueryBuilder<T>
    {
        private PagedSearchDTO<T> pagedSearchDTO = new PagedSearchDTO<T>();

        public QueryBuilder<T> WithDTO(PagedSearchDTO<T> pagedSearchDTO)
        {
            this.pagedSearchDTO = pagedSearchDTO;
            return this;
        }

        public String GetOrderBy(String alias)
        {
            string sortField = GetRealColumnName(pagedSearchDTO.SortFields);
            return $" order by {alias}.{sortField} {pagedSearchDTO.SortDirections}";
        }

        public String GetOffSet()
        {
            return $" offset {GetStart()} rows fetch next {pagedSearchDTO.GetPageSize()} rows only";
        }


        public int GetStart()
        {
            return (pagedSearchDTO.GetCurrentPage() - 1) * pagedSearchDTO.GetPageSize();
        }

        public String GetWhereAndParameters(String alias)
        {
            String query = " where ";
            foreach (var entry in pagedSearchDTO.Filters)
            {
                if (KeyAndValueIsNotNull(entry))
                {
                    String key = entry.Key;
                    Object value = entry.Value;

                    String column = GetRealColumnName(key);

                    if (IsDateTimeType(value.ToString()))
                    {
                        query = query + "CAST(REPLACE(" + alias + "." + column + ", '-', '') AS DATE) = CAST(REPLACE('" + value + "', '-', '') AS DATE) and ";
                    }
                    else if (value.GetType() == typeof(int))
                    {
                        query = query + alias + "." + column + " = " + value + " and ";
                    }
                    else if (value.GetType() == typeof(double))
                    {
                        query = query + alias + "." + column + " = '" + value + "' and ";
                    }
                    else if (value.GetType() == typeof(string))
                    {
                        query = query + alias + "." + column + " like '%" + value + "%'" + " and ";
                    }
                    else
                    {
                        query = query + alias + "." + column + " = " + value + " and ";
                    }
                }
            }
            query = query + "1 = 1 ";
            return query;
        }

        private static bool KeyAndValueIsNotNull(KeyValuePair<string, object> entry)
        {
            return entry.Key != null && entry.Value != null && !String.IsNullOrEmpty(entry.Key.ToString()) && !String.IsNullOrEmpty(entry.Value.ToString());
        }

        public String GetQueryFromDTO(String alias, String entityName)
        {
            return GetBaseSelect(alias, entityName) + GetWhereAndParameters(alias) + GetOrderBy(alias) + GetOffSet();
        }

        public String GetQueryFromDTOWithColumnAlias(String columnsAlias, String alias, String entityName)
        {
            return GetBaseSelectWithColumnAlias(columnsAlias, alias, entityName) + GetWhereAndParameters(alias) + GetOrderBy(alias) + GetOffSet();
        }

        public String GetQueryFromDTOWithColumnAlias(String alias, String entityName)
        {
            string columnsAlias = GetColumnsAlias();
            return GetBaseSelectWithColumnAlias(columnsAlias, alias, entityName) + GetWhereAndParameters(alias) + GetOrderBy(alias) + GetOffSet();
        }


        public String GetBaseSelectWithColumnAlias(String columnsAlias, String alias, String entityName)
        {
            return $"select {columnsAlias} from {entityName} {alias} ";
        }

        public String GetBaseSelect(String alias, String entityName)
        {
            return $"select * from {entityName} {alias} ";
        }

        public String GetBaseSelectCount(String alias, String entityName)
        {
            return $"select count(*) from {entityName} {alias} ";
        }

        public String GetRealColumnName(string attributeName)
        {
            var myType = typeof(T);

            var myProperty = myType.GetProperty(attributeName);
            if (myProperty == null) return attributeName;

            var column = (ColumnAttribute)myProperty.GetCustomAttribute(typeof(ColumnAttribute));
            if (column == null || column.Name == null) return attributeName;

            return column.Name;
        }

        //SEE: https://www.codeproject.com/Articles/742461/Csharp-Using-Reflection-and-Custom-Attributes-to-M
        public String GetColumnsAlias()
        {
            string columns = "";

            PropertyInfo[] props = typeof(T).GetProperties();
            Trace.WriteLine(props);
            foreach (PropertyInfo prop in props)
            {
                string propName = prop.Name;

                var attribute = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute));
                if (attribute != null)
                {
                    var realColumnName = attribute.Name;
                    columns = columns + $" {realColumnName} as {propName},";
                }
            }

            return columns.Remove(columns.Length - 1, 1) + " ";
        }

        public bool IsDateTimeType(string value)
        {
            try
            {
                DateTime dt = DateTime.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}