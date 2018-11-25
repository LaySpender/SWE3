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
    public class MSSQLDeleteProvider : ISQLDeleteProvider
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

        public MSSQLDeleteProvider(object obj)
        {
            table = obj.GetType().Name;

            PropertyInfo[] properties = obj.GetType().GetProperties();
            var columns = new List<string>();
            var values = new List<string>();
            primaryKeyProperty = "";
            foreach (var property in properties)
            {
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is ColumnAttribute)
                    {
                        if (attribute is PrimaryKeyAttribute)
                        {
                            var primaryKeyAttr = (PrimaryKeyAttribute)attribute;
                            primaryKeyProperty = primaryKeyAttr.Name;
                            primaryKeyValue = ValueFormatter.FormatForQuery(property.GetValue(obj));
                        }
                        else
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)attribute;
                            columns.Add(columnAttr.Name);
                            values.Add(ValueFormatter.FormatForQuery(property.GetValue(obj)));
                        }                        
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(primaryKeyProperty))
            {
                statement = $"DELETE FROM {table} WHERE {primaryKeyProperty} = {primaryKeyValue}";
            }
            else
            {
                throw new NoPrimaryKeyException();
            }
        }

        private string Statement => statement;

        public void DeleteObject()
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
                throw new Exception("Exception while deleting data in db.", ex);
            }
        }
    }
}
