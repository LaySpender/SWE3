using LinqORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqORM
{
    public class SQLStatementBuilder
    {
        public SQLStatementBuilder()
        {
            select = "SELECT";
            columns = new List<string>();
            from = "FROM";
            condition = "";
        }

        private readonly string select;
        public string Select { get => select; }

        private List<string> columns;
        public List<string> Columns { get => columns; set => columns = value; }

        private readonly string from;
        public string From { get => from; }

        public string Table { get; set; }
        
        private string condition;
        public string Condition { get => condition; set => condition = value; }

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
