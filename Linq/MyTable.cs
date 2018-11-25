using System;
using System.Collections.Generic;
using System.Text;
using LinqORM.Attributes;

namespace LinqORM
{
    class MyTable
    {
        [Column(Name = "FirstName")]
        public string FirstName { get; set; }
        [Column(Name = "Name")]
        public string LastName { get; set; }
        [Column]
        public int Age { get; set; }
        [NoDataMember]
        public int NoColumn { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age}";
        }
    }
}
