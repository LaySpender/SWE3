using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace LinqORM
{
    /// <summary>
    /// Class for creating a Queryable for the Object-Relational-Mapper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Linq.IQueryable{T}" />
    public class LinqQueryable<T> : IQueryable<T>
    {
        private Expression _expression = null;
        private LinqQueryProvider _provider = null;
        private ORM _orm = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinqQueryable{T}"/> class.
        /// </summary>
        /// <param name="orm">The orm.</param>
        public LinqQueryable(ORM orm)
        {
            _expression = Expression.Constant(this);
            _provider = new LinqQueryProvider(orm);
            _orm = orm;
        }

        internal LinqQueryable(LinqQueryProvider provider, Expression expression)
        {
            _expression = expression;
            _provider = provider;
        }

        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        /// <value>
        /// The type of the element.
        /// </value>
        public Type ElementType => typeof(T);

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public Expression Expression => _expression;

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public IQueryProvider Provider => _provider;

        /// <summary>
        /// Gets the orm.
        /// </summary>
        /// <value>
        /// The orm.
        /// </value>
        public ORM Orm => _orm;

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
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
