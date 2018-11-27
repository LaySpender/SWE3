using LinqORM;
using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM_Test
{
    internal class TestSetup
    {
        internal class TestTableWithId : ObservableObject
        {
            private int id;
            private string bezeichnung;
            private string ort;
            private string strasse;

            [PrimaryKey]
            public int Id { get => id; set => SetProperty(ref id, value); }
            [Column]
            public string Bezeichnung { get => bezeichnung; set => SetProperty(ref bezeichnung, value); }
            [Column]
            public string Ort { get => ort; set => SetProperty(ref ort, value); }
            [Column]
            public string Strasse { get => strasse; set => SetProperty(ref strasse, value); }
        }

        internal class TestTableWithoutId : ObservableObject
        {
            private string bezeichnung;
            private string ort;
            private string strasse;

            [Column]
            public string Bezeichnung { get => bezeichnung; set => SetProperty(ref bezeichnung, value); }
            [Column]
            public string Ort { get => ort; set => SetProperty(ref ort, value); }
            [Column]
            public string Strasse { get => strasse; set => SetProperty(ref strasse, value); }
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
