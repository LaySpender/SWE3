using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using LinqORM.Extensions;
using LinqORM.Helpers;
using System.Data.SqlClient;
using LinqORM.Interfaces;
using LinqORM.Exceptions;

namespace LinqORM.MSSQL
{
    public class MSSQLInsertProvider : ISQLInsertProvider
    {
        string table;
        string statement;
        string primaryKeyProperty;
        private readonly string connectionString =
            "Data Source=GIGAMATE;" +
            "Initial Catalog=Produktion;" +
            "User id=orm;" +
            "Password=c#;";

        public MSSQLInsertProvider(object obj)
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
            if(!string.IsNullOrWhiteSpace(primaryKeyProperty))
            {
                statement = $"INSERT INTO {table} ({columns.GetEntriesSeperatedBy(",")}) OUTPUT Inserted.{primaryKeyProperty} VALUES ({values.GetEntriesSeperatedBy(",")})";
            }
            else
            {
                throw new NoPrimaryKeyException();
            }
        }

        private string Statement => statement;

        public void InsertObject(object obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Statement, con);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    obj.GetType().GetProperty(primaryKeyProperty).SetValue(obj, reader["ID"]);
                        
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while inserting data in db.", ex);
            }
        }
    }
}
