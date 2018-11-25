using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication
{
    public class Werke
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Column]
        public string Bezeichnung { get; set; }
        [Column]
        public string Ort { get; set; }
        [Column]
        public string Strasse { get; set; }
    }
}
