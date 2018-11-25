using System;
using System.Linq;

namespace LinqORM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Linq!");

            // ----------------
            // for testing purpose only

            Test test1 = new Test(18);
            Test test2 = new Test(30);
            Test test3 = new Test("Peter");

            // ----------------

            Queryable<MyTable> qry = new Queryable<MyTable>();

            IQueryable<MyTable> filtered = qry
                .Where(i => i.Age > 18 && i.Age < 30)
                .Where(i => i.FirstName == "Peter");

            System.Collections.Generic.List<MyTable> lst = filtered.ToList();

            foreach(MyTable i in lst)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }
    }
}
