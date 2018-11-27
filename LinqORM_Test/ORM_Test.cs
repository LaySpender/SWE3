using System;
using Xunit;
using LinqORM;
using static LinqORM_Test.TestSetup;
using System.Linq;
using System.Linq.Expressions;
using LinqORM.Exceptions;
using LinqORM.Helpers;
using System.Collections.Generic;
using LinqORM.Extensions;

namespace LinqORM_Test
{
    public class ORM_Test
    {
        #region LinqQueryable
        [Fact]
        public void GetQuery_OfTypeTestTableWithId_ReturnsQueryable()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
        }

        [Fact]
        public void GetQuery_OfTypeTestTableWithId_HasORM()
        {
            // Arrange
            var orm = new ORM();
            LinqQueryable<TestTableWithId> qry2;
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            qry2 = (LinqQueryable<TestTableWithId>)qry;
            Assert.NotNull(qry2.Orm);
        }

        [Fact]
        public void GetQuery_OfTypeTestTableWithId_CallerORMEqualsQueryORM()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            var qry2 = (LinqQueryable<TestTableWithId>)qry;
            Assert.Equal<ORM>(orm, qry2.Orm);
        }

        #region ElementType
        [Fact]
        public void GetQuery_OfTypeTestTableWithId_ElementTypeNotNull()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            Assert.NotNull(qry.ElementType);
        }

        [Fact]
        public void GetQuery_OfTypeTestTableWithId_ElementTypeEqualsWantedTable()
        {
            // Arrange
            var orm = new ORM();
            Type t = typeof(TestTableWithId);
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            Assert.NotNull(qry.ElementType);
            Assert.Equal(t, qry.ElementType);
        }
        #endregion

        #region Expression
        [Fact]
        public void GetQuery_OfTypeTestTableWithId_ExpressionNotNull()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            Assert.NotNull(qry.Expression);
        }

        [Fact]
        public void GetQuery_OfTypeTestTableWithId_ExpressionOfTypeConstantExpression()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            Assert.NotNull(qry.Expression);
            Assert.IsType<ConstantExpression>(qry.Expression);
        }
        #endregion

        #region Provider
        [Fact]
        public void GetQuery_OfTypeTestTableWithId_ProviderNotNull()
        {
            // Arrange
            var orm = new ORM();
            // Act
            var qry = orm.GetQuery<TestTableWithId>();
            // Assert
            Assert.NotNull(qry);
            Assert.IsType<LinqQueryable<TestTableWithId>>(qry);
            Assert.NotNull(qry.Provider);
        }
        #endregion

        #endregion

        #region ORM

        #region Delete
        [Fact]
        public void Delete_AlreadyAttachedObj_ThrowsActionsNotExecutedException()
        {
            // Arrange
            Exception ex;
            var orm = new ORM();
            var ttwi = new TestTableWithId();
            orm.Attach(ttwi);
            // Act
            ex = Record.Exception(() => orm.Delete(ttwi));
            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ActionsNotExecutedException>(ex);
        }
        #endregion

        #region Attach
        [Fact]
        public void Attach_AlreadyDeletedObj_ThrowsActionsNotExecutedException()
        {
            // Arrange
            Exception ex;
            var orm = new ORM();
            var ttwi = new TestTableWithId();
            orm.Attach(ttwi);
            // Act
            ex = Record.Exception(() => orm.Attach(ttwi));
            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ActionsNotExecutedException>(ex);
        }
        #endregion

        #endregion

        #region Helper
        [Fact]
        public void ValueFormatter_Int_IntValueAsString()
        {
            // Arrange
            int i = 8;
            // Act
            var formatted = ValueFormatter.FormatForQuery(i);
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(i.ToString(), formatted);
        }

        [Fact]
        public void ValueFormatter_Decimal_DecimalValueAsString()
        {
            // Arrange
            decimal i = 8;
            // Act
            var formatted = ValueFormatter.FormatForQuery(i);
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(i.ToString(), formatted);
        }

        [Fact]
        public void ValueFormatter_Double_DoubleValueAsString()
        {
            // Arrange
            double i = 8;
            // Act
            var formatted = ValueFormatter.FormatForQuery(i);
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(i.ToString(), formatted);
        }

        [Fact]
        public void ValueFormatter_String_StringValueAsStringWithParantheses()
        {
            // Arrange
            string i = "test";
            // Act
            var formatted = ValueFormatter.FormatForQuery(i);
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal($"'{i}'", formatted);
        }
        #endregion

        #region Extensions

        #region ListExtension
        [Fact]
        public void ListExtension_String_StringSeperatedByKoma()
        {
            // Arrange
            string expected = "A,B,C,D,E";
            var lst = new List<string>()
            {
                "A", "B", "C", "D", "E",
            };
            // Act
            var formatted = lst.GetEntriesSeperatedBy(",");
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(expected, formatted);
        }

        [Fact]
        public void ListExtension_Int_StringSeperatedByKoma()
        {
            // Arrange
            string expected = "1,1,4,2235,23";
            var lst = new List<int>()
            {
                1, 1, 4, 2235, 23,
            };
            // Act
            var formatted = lst.GetEntriesSeperatedBy(",");
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(expected, formatted);
        }

        [Fact]
        public void ListExtension_String_StringSeperatedByAnything()
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();
            string expected = "A" + guid + "B" + guid + "C";
            var lst = new List<string>()
            {
                "A", "B", "C",
            };
            // Act
            var formatted = lst.GetEntriesSeperatedBy(guid);
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(expected, formatted);
        }
        #endregion

        #region DictionaryExtension
        [Fact]
        public void DictionaryExtension_OneStringAndInt_EntriesForSQLUpdate()
        {
            // Arrange
            string expected = "A = 8";
            int i = 8;
            var dict = new Dictionary<string, int>();
            dict.Add("A", i);
            // Act
            var formatted = dict.GetEntriesForSQLUpdate();
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(expected, formatted);
        }

        [Fact]
        public void DictionaryExtension_MultipleStringAndInt_EntriesForSQLUpdate()
        {
            // Arrange
            string expected = "A = 8,B = 10,C = 12,D = 14";
            int i = 8;
            var dict = new Dictionary<string, int>();
            dict.Add("A", i);
            dict.Add("B", i+2);
            dict.Add("C", i+4);
            dict.Add("D", i+6);
            // Act
            var formatted = dict.GetEntriesForSQLUpdate();
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(expected, formatted);
        }

        [Fact]
        public void DictionaryExtension_MultipleStringAndObject_EntriesForSQLUpdate()
        {
            // Arrange
            string expected = "A = 8,B = 't',C = 3";
            int i = 8;
            string i2 = "'t'";
            decimal i3 = 3;
            var dict = new Dictionary<string, object>();
            dict.Add("A", i);
            dict.Add("B", i2);
            dict.Add("C", i3);
            // Act
            var formatted = dict.GetEntriesForSQLUpdate();
            // Assert
            Assert.NotNull(formatted);
            Assert.Equal(expected, formatted);
        }
        #endregion

        #endregion
    }
}
