using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace LinqORM
{
    public class Queryable<T> : IQueryable<T>
    {
        private Expression _expression = null;
        private QueryProvider _provider = null;
        public Queryable()
        {
            _expression = Expression.Constant(this);
            _provider = new QueryProvider();
        }

        internal Queryable(QueryProvider provider, Expression expression)
        {
            _expression = expression;
            _provider = provider;
        }

        public Type ElementType => typeof(T);

        public Expression Expression => _expression;

        public IQueryProvider Provider => _provider;

        public IEnumerator<T> GetEnumerator()
        {
            // Returns a enumeration (ToList, ToArray, foreach, ...)
            return _provider.GetEnumerator<T>(_expression);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Returns a enumeration (ToList, ToArray, foreach, ...)
            return _provider.GetEnumerator<T>(_expression);
        }
    }
}
