using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqORM
{
    internal class QueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Queryable<TElement>(this, expression);
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
            var visitor = new DemoExpressionTreeVisitor<T>();
            visitor.Visit(expression);

            Console.WriteLine(visitor.SqlBuilder.SelectStatement);

            // mocking result of query
            return new MyTable[]
            {
                new MyTable() { FirstName = "Peter", Age = 28 },
            }
            .OfType<T>()
            .GetEnumerator();
        }
    }
}
