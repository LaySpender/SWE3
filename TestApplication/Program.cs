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
            SelectOne();
            InsertOne();
            DeleteOne();
            UpdateOne();
            //TableDoesNotExist();
            Console.ReadKey();
        }
        
        private static void SelectOne()
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
            Console.ReadKey();
            Console.Clear();
        }

        private static void InsertOne()
        {
            ORM orm = new ORM();

            var newObj = new Werke();
            newObj.Bezeichnung = "Eckenwerk";
            newObj.Ort = "Graz";
            newObj.Strasse = "Eckstraße";

            orm.Attach(newObj);
            Console.WriteLine("Created object.");
            orm.SaveChanges();
            Console.WriteLine("Saved object.");

            IQueryable<Werke> qry = orm.GetQuery<Werke>();

            IQueryable<Werke> filtered = qry
                .Where(i => i.Bezeichnung == "Eckenwerk");
        

            List<Werke> lst = filtered.ToList();

            foreach (var i in lst)
            {
                Console.WriteLine($"{i.Id}, {i.Bezeichnung}, {i.Ort}, {i.Strasse}");
            }

            Console.ReadKey();
            Console.Clear();
        }

        private static void DeleteOne()
        {
            ORM orm = new ORM();

            var newObj = new Werke();
            newObj.Bezeichnung = "Eckenwerk";
            newObj.Ort = "Graz";
            newObj.Strasse = "Eckstraße";

            orm.Attach(newObj);
            Console.WriteLine("Created object.");
            orm.SaveChanges();
            Console.WriteLine("Saved object.");

            IQueryable<Werke> qry = orm.GetQuery<Werke>();

            IQueryable<Werke> filtered = qry
                .Where(i => i.Bezeichnung == "Eckenwerk");
            
            List<Werke> lst = filtered.ToList();

            foreach (var i in lst)
            {
                Console.WriteLine($"{i.Id}, {i.Bezeichnung}, {i.Ort}, {i.Strasse}");
            }

            orm.Delete(newObj);
            Console.WriteLine("Add to delete object.");
            orm.SaveChanges();
            Console.WriteLine("Deleted object.");

            IQueryable<Werke> qry2 = orm.GetQuery<Werke>();

            IQueryable<Werke> filtered2 = qry2
                .Where(i => i.Bezeichnung == "Eckenwerk");


            List<Werke> lst2 = filtered2.ToList();

            foreach (var i in lst2)
            {
                Console.WriteLine($"{i.Id}, {i.Bezeichnung}, {i.Ort}, {i.Strasse}");
            }

            Console.ReadKey();
            Console.Clear();
        }

        private static void UpdateOne()
        {
            ORM orm = new ORM();

            IQueryable<Werke> qry = orm.GetQuery<Werke>();

            IQueryable<Werke> filtered = qry
                .Where(i => i.Id == 14);

            List<Werke> lst = filtered.ToList();

            foreach (var i in lst)
            {
                Console.WriteLine($"{i.Id}, {i.Bezeichnung}, {i.Ort}, {i.Strasse}");
            }
            var newObj = lst[0];
            newObj.Ort = "Eckenberger";

            orm.SaveChanges();
            Console.WriteLine("Updated object.");

            IQueryable<Werke> qry2 = orm.GetQuery<Werke>();

            IQueryable<Werke> filtered2 = qry2
                .Where(i => i.Id == 14);


            List<Werke> lst2 = filtered2.ToList();

            foreach (var i in lst2)
            {
                Console.WriteLine($"{i.Id}, {i.Bezeichnung}, {i.Ort}, {i.Strasse}");
            }

            Console.ReadKey();
            Console.Clear();
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
