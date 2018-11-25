using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM_Test
{
    internal class TestSetup
    {
        internal class TestTable
        {
            [Column]
            public string FirstName { get; set; }
            [Column]
            public string LastName { get; set; }
            [Column]
            public int Age { get; set; }
        }

        internal class NoColumnAttrTestTable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        // todo: delete statements and create expressions that create statement
        public string invalidStatement = "";
        public string validStatement = "SELECT FirstName, LastName, Age FROM TestTable Where (Age > 1)";
        public string validComplexStatement = "SELECT FirstName, LastName, Age FROM TestTable Where (Age > 1) AND ((FirstName = 'Peter') OR (LastName = 'Franz'))";
    }
}
