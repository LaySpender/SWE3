using LinqORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqORM.Interfaces;

namespace LinqORM
{
    /// <summary>
    /// Class for Building MSSQL select statements
    /// </summary>
    /// <seealso cref="LinqORM.Interfaces.ISQLSelectBuilder" />
    public class MSSQLSelectBuilder : ISQLSelectBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLSelectBuilder"/> class.
        /// </summary>
        public MSSQLSelectBuilder()
        {
            select = "SELECT";
            columns = new List<string>();
            from = "FROM";
            condition = "";
        }

        private readonly string select;
        /// <summary>
        /// Gets the select.
        /// </summary>
        /// <value>
        /// The select.
        /// </value>
        public string Select { get => select; }

        private List<string> columns;
        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public List<string> Columns { get => columns; set => columns = value; }

        private readonly string from;
        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From { get => from; }

        /// <summary>
        /// Gets or sets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        public string Table { get; set; }
        
        private string condition;
        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public string Condition { get => condition; set => condition = value; }

        /// <summary>
        /// Gets the select statement.
        /// </summary>
        /// <value>
        /// The select statement.
        /// </value>
        public string SelectStatement
        {
            get
            {
                string result = Select;
                result = AddStrToStatement(result, Columns.GetEntriesSeperatedBy(", "));
                result = AddStrToStatement(result, From);
                result = AddStrToStatement(result, Table);
                if(!string.IsNullOrWhiteSpace(Condition))
                {
                    result = AddStrToStatement(result, Condition);
                }
                return result;
            }
        }

        private string AddStrToStatement(string statement, string str)
        {
            string result = statement + " " + str;
            return result;
        }
    }
}
