using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using LinqORM.Interfaces;

namespace LinqORM.MSSQL
{
    /// <summary>
    /// Class for communication with mssql database regarding select statements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LinqORM.Interfaces.ISQLSelectProvider" />
    public class MSSQLSelectProvider<T> : ISQLSelectProvider
    {
        private MSSQLSelectVisitor<T> visitor;
        private MSSQLSelectBuilder sqlInfo;
        private readonly string connectionString = 
            "Data Source=GIGAMATE;" + 
            "Initial Catalog=Produktion;" +
            "User id=orm;" +
            "Password=c#;";

        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLSelectProvider{T}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public MSSQLSelectProvider(Expression expression)
        {
            visitor = new MSSQLSelectVisitor<T>();
            visitor.Visit(expression);

            Console.WriteLine(visitor.SqlBuilder.SelectStatement);
            Statement = visitor.SqlBuilder.SelectStatement;
            sqlInfo = visitor.SqlBuilder;
        }

        /// <summary>
        /// Gets the statement.
        /// </summary>
        /// <value>
        /// The statement.
        /// </value>
        public string Statement { get; }

        /// <summary>
        /// Selects the objects.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Exception while getting data from db.</exception>
        public IEnumerable<Dictionary<string, object>> GetObjects()
        {
            List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Statement, con);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            dict.Add(sqlInfo.Columns[j], reader[sqlInfo.Columns[j]]);
                        }
                        objects.Add(dict);
                    }
                    con.Close();
                }
                return objects;
            }
            catch(Exception ex)
            {
                throw new Exception("Exception while getting data from db.", ex);
            }
        }
    }
}
