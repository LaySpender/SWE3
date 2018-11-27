using LinqORM.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqORM
{
    internal class LinqQueryProvider : IQueryProvider
    {
        ORM _orm;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinqQueryProvider"/> class.
        /// </summary>
        /// <param name="orm">The orm.</param>
        public LinqQueryProvider(ORM orm)
        {
            _orm = orm;
        }

        /// <summary>
        /// Constructs an <see cref="T:System.Linq.IQueryable"></see> object that can evaluate the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// An <see cref="System.Linq.IQueryable"></see> that can evaluate the query represented by the specified expression tree.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructs an <see cref="T:System.Linq.IQueryable`1"></see> object that can evaluate the query represented by a specified expression tree.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements of the <see cref="T:System.Linq.IQueryable`1"></see> that is returned.</typeparam>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// An <see cref="System.Linq.IQueryable`1"></see> that can evaluate the query represented by the specified expression tree.
        /// </returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new LinqQueryable<TElement>(this, expression);
        }

        /// <summary>
        /// Executes the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// The value that results from executing the specified query.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Execute(Expression expression)
        {
            // returns a single object (First, Single, etc)
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the strongly-typed query represented by a specified expression tree.
        /// </summary>
        /// <typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// The value that results from executing the specified query.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
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