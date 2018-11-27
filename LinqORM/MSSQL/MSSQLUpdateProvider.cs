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
    /// <summary>
    /// Class for communication with mssql database regarding update statements.
    /// </summary>
    /// <seealso cref="LinqORM.Interfaces.ISQLUpdateProvider" />
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLUpdateProvider"/> class.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="NoPrimaryKeyException"></exception>
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
                        if (attribute is PrimaryKeyAttribute)
                        {
                            var primaryKeyAttr = (PrimaryKeyAttribute)attribute;
                            primaryKeyProperty = primaryKeyAttr.Name;
                            primaryKeyValue = ValueFormatter.FormatForQuery(property.GetValue(obj));
                        }
                        else
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)attribute;
                            columnsValues.Add(columnAttr.Name, ValueFormatter.FormatForQuery(property.GetValue(obj)));
                        }                        
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

        /// <summary>
        /// Gets the statement.
        /// </summary>
        /// <value>
        /// The statement.
        /// </value>
        public string Statement => statement;

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="Exception">Exception while updating data in db.</exception>
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
