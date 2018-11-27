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
    /// <summary>
    /// Class for communication with mssql database regarding insert statements.
    /// </summary>
    /// <seealso cref="LinqORM.Interfaces.ISQLInsertProvider" />
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLInsertProvider"/> class.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="NoPrimaryKeyException"></exception>
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

        /// <summary>
        /// Gets the statement.
        /// </summary>
        /// <value>
        /// The statement.
        /// </value>
        public string Statement => statement;

        /// <summary>
        /// Inserts the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="Exception">Exception while inserting data in db.</exception>
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
