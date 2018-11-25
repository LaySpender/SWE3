using LinqORM.Attributes;
using LinqORM.Exceptions;
using LinqORM.Extensions;
using LinqORM.Helpers;
using LinqORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace LinqORM.MSSQL
{
    public class MSSQLUpdateProvider : ISQLUpdateProvider
    {
        string table;
        string statement;
        string primaryKeyProperty;
        string primaryKeyValue;
        private readonly string connectionString =
            "Data Source=GIGAMATE;" +
            "Initial Catalog=Produktion;" +
            "User id=orm;" +
            "Password=c#;";

        public MSSQLUpdateProvider(object obj)
        {
            table = obj.GetType().Name;

            PropertyInfo[] properties = obj.GetType().GetProperties();
            var columnsValues = new Dictionary<string, object>();
            primaryKeyProperty = "";
            foreach (var property in properties)
            {
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is ColumnAttribute)
                    {
                        ColumnAttribute columnAttr = (ColumnAttribute)attribute;
                        columnsValues.Add(columnAttr.Name, ValueFormatter.FormatForQuery(property.GetValue(obj)));
                    }
                    else if (attribute is PrimaryKeyAttribute)
                    {
                        var primaryKeyAttr = (PrimaryKeyAttribute)attribute;
                        primaryKeyProperty = primaryKeyAttr.Name;
                        primaryKeyValue = ValueFormatter.FormatForQuery(property.GetValue(obj));
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(primaryKeyProperty))
            {
                statement = $"UPDATE {table} SET {columnsValues.GetEntriesForSQLUpdate()} WHERE {primaryKeyProperty} = {primaryKeyValue}";
            }
            else
            {
                throw new NoPrimaryKeyException();
            }
        }

        private string Statement => statement;

        public void UpdateObject(object obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Statement, con);
                    command.ExecuteReader();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while updating data in db.", ex);
            }
        }
    }
}
