using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace LinqORM.Mock
{
    public class MockQueryable<T> : IQueryable<T>
    {
        private Expression _expression = null;
        private MockQueryProvider _provider = null;
        private ORM _orm = null;

        public MockQueryable(ORM orm)
        {
            _expression = Expression.Constant(this);
            _provider = new MockQueryProvider(orm);
            _orm = orm;
        }

        internal MockQueryable(MockQueryProvider provider, Expression expression)
        {
            _expression = expression;
            _provider = provider;
        }

        public Type ElementType => typeof(T);

        public Expression Expression => _expression;

        public IQueryProvider Provider => _provider;

        public ORM Orm => _orm;

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
