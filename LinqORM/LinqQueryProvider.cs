using LinqORM.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqORM
{
    public class LinqQueryProvider : IQueryProvider
    {
        ORM _orm;

        public LinqQueryProvider(ORM orm)
        {
            _orm = orm;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new LinqQueryable<TElement>(this, expression);
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