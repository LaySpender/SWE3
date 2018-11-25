using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication
{
    public class Test
    {
        public Test(int i)
        {
            Id = i;
        }

        public Test(string str)
        {
            Name = str;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
