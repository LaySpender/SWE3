using System;
using Xunit;
using LinqORM;
using static LinqORM_Test.TestSetup;
using System.Linq;
using System.Linq.Expressions;

namespace LinqORM_Test
{
    public class ORM_Test
    {
        #region LinqQueryable
        [Fact]
        public void GetQuery_OfTypeTestTable_ReturnsQueryable()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
        }

        [Fact]
        public void GetQuery_OfTypeTestTable_HasORM()
        {
            // Arrange
            var orm = new ORM();
            LinqQueryable<TestTable> qry2;
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            qry2 = (LinqQueryable<TestTable>)qry;
            Assert.NotNull(qry2.Orm);
        }

        [Fact]
        public void GetQuery_OfTypeTestTable_CallerORMEqualsQueryORM()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            var qry2 = (LinqQueryable<TestTable>)qry;
            Assert.Equal<ORM>(orm, qry2.Orm);
        }

        #region ElementType
        [Fact]
        public void GetQuery_OfTypeTestTable_ElementTypeNotNull()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            Assert.NotNull(qry.ElementType);
        }

        [Fact]
        public void GetQuery_OfTypeTestTable_ElementTypeEqualsWantedTable()
        {
            // Arrange
            var orm = new ORM();
            Type t = typeof(TestTable);
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            Assert.NotNull(qry.ElementType);
            Assert.Equal(t, qry.ElementType);
        }
        #endregion

        #region Expression
        [Fact]
        public void GetQuery_OfTypeTestTable_ExpressionNotNull()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            Assert.NotNull(qry.Expression);
        }

        [Fact]
        public void GetQuery_OfTypeTestTable_ExpressionOfTypeConstantExpression()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            Assert.NotNull(qry.Expression);
            Assert.IsType<ConstantExpression>(qry.Expression);
        }
        #endregion

        #region Provider
        [Fact]
        public void GetQuery_OfTypeTestTable_ProviderNotNull()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTable>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTable>>(qry);
            Assert.NotNull(qry.Provider);
        }
        #endregion

        #endregion


        #region SQLProvider

        #endregion
    }
}
