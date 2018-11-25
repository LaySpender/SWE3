using LinqORM.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqORM.Mock
{
    class MockQueryProvider : IQueryProvider
    {
        ORM _orm;

        public MockQueryProvider(ORM orm)
        {
            _orm = orm;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MockQueryable<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            // returns a single object (First, Single, etc)
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            // returns a single object (First, Single, etc)
            throw new NotImplementedException();
        }

        internal IEnumerator<T> GetEnumerator<T>(Expression expression)
        {
            // Returns a enumeration (ToList, ToArray, foreach, ...)
            var sqlSelectProvider = _orm.GetSelectProvider<T>(expression);
            IEnumerable<T> result = _orm.MaterializeObjects<T>(sqlSelectProvider.GetObjects());

            return result.GetEnumerator();
        }
    }
}
