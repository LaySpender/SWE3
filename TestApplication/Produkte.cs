using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication
{
    public class Produkte
    {
        [PrimaryKey]
        [Column]
        public int Id { get; set; }
        [Column]
        public int WerkId { get; set; }
        [Column]
        public string Bezeichnung { get; set; }
    }
}
