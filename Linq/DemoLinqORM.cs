using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqORM
{
    class DemoLinqORM
    {
        public IQueryable<T> GetQuery<T>() 
        {
            return new Queryable<T>();
        }

        public void Attach<T>(T obj)
        {

        }

        public void Delete<T>(T obj)
        {

        }

        public void SaveChanges<T>()
        {

        }
    }
}
