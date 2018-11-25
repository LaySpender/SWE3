using LinqORM;
using System;
using System.Linq;
using System.Collections.Generic; 
using TestApplication;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Linq!");
            SelectWithWhereString();
            //TableDoesNotExist();
            Console.ReadKey();
        }

        private static void SelectWithWhereString()
        {
            ORM orm = new ORM();

            IQueryable<Produkte> qry = orm.GetQuery<Produkte>();

            IQueryable<Produkte> filtered = qry
                .Where(i => i.Bezeichnung == "Fußball");

            List<Produkte> lst = filtered.ToList();

            foreach (var i in filtered)
            {
                Console.WriteLine($"{i.Id}, {i.WerkId}, {i.Bezeichnung}");
            }
        }

        private static void TableDoesNotExist()
        {
            Test test1 = new Test(18);
            Test test2 = new Test(30);
            Test test3 = new Test("Peter");

            ORM orm = new ORM();

            IQueryable<MyTable> qry = orm.GetQuery<MyTable>();

            IQueryable<MyTable> filtered = qry
                .Where(i => i.Age > 18 && i.Age < 30)
                .Where(i => i.FirstName == "Peter");

            List<MyTable> lst = filtered.ToList();

            foreach(var i in filtered)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}
