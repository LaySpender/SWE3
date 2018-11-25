using System;
using System.Collections.Generic;
using System.Text;
using LinqORM.Attributes;

namespace LinqORM
{
    class MyTable
    {
        [Column]
        [PrimaryKey]
        public int Id { get; set; }
        [Column(name: "FirstName")]
        public string FirstName { get; set; }
        [Column(name: "Name")]
        public string LastName { get; set; }
        [Column]
        public int Age { get; set; }
        [NoDataMember]
        public int NoColumn { get; set; }
    }
}
