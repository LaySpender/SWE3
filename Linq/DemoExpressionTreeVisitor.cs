using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LinqORM
{
    public class DemoExpressionTreeVisitor<T> : ExpressionTreeVisitor
    {
        private SQLStatementBuilder _sqlBuilder = new SQLStatementBuilder();
        public SQLStatementBuilder SqlBuilder { get => _sqlBuilder; set => _sqlBuilder = value; }

        public DemoExpressionTreeVisitor()
        {
            SqlBuilder.Table = typeof(T).Name;
            PropertyInfo[] properties = typeof(T).GetProperties();
            var columns = new List<string>();
            foreach(var property in properties)
            {
                if (property.CustomAttributes.Any())
                {
                    foreach(var attribute in property.GetCustomAttributes())
                    {
                        if (attribute is ColumnAttribute)
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)attribute;
                            columns.Add((string.IsNullOrWhiteSpace(columnAttr.Name) ? property.Name : columnAttr.Name));                   
                        }
                    }
                }
            }
            SqlBuilder.Columns = columns;
        }
        
        public override Expression Visit(Expression e)
        {
            if (e != null)
            {
                // Console.WriteLine(e.NodeType);
            }
            return base.Visit(e);
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            // with m.Method.Name get name of Method (f.e.: m.Method.Name == "Where")
            //m.Method.Name
            if (m.Type != null && m.Type.Name == typeof(IQueryable<>).Name && m.Method.Name == "Where")
            {
                Visit(m.Arguments[0]);
                if(!SqlBuilder.Condition.Contains(m.Method.Name))
                {
                    SqlBuilder.Condition += $"{m.Method.Name} ";
                }
                else
                {
                    SqlBuilder.Condition += "AND ";
                }
                return Visit(m.Arguments[1]);
            }

            return base.VisitMethodCall(m);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            SqlBuilder.Condition += "( ";
            Visit(b.Left);

            switch (b.NodeType)
            {
                case ExpressionType.AndAlso:
                    SqlBuilder.Condition += "AND ";
                    break;
                case ExpressionType.OrElse:
                    SqlBuilder.Condition += "OR ";
                    break;
                case ExpressionType.LessThan:
                    SqlBuilder.Condition += "< ";
                    break;
                case ExpressionType.LessThanOrEqual:
                    SqlBuilder.Condition += "<= ";
                    break;
                case ExpressionType.GreaterThan:
                    SqlBuilder.Condition += "> ";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    SqlBuilder.Condition += ">= ";
                    break;
                case ExpressionType.Equal:
                    SqlBuilder.Condition += "= ";
                    break;
                case ExpressionType.NotEqual:
                    SqlBuilder.Condition += "<> ";
                    break;
                default:
                    throw new NotImplementedException();
            }

            Visit(b.Right);

            SqlBuilder.Condition += ") ";
            return b;
            // return base.VisitBinary(b);
        }

        protected override Expression VisitMemberAccess(MemberExpression e)
        {
            if (e.Expression.NodeType == ExpressionType.Constant)
            {
                var ce = (ConstantExpression)e.Expression;
                var fi = ce.Type.GetField(e.Member.Name);
                SqlBuilder.Condition += fi.GetValue(ce.Value) + " ";

            }
            else
            {
                SqlBuilder.Condition += e.Member.Name + " ";
            }

            return base.VisitMemberAccess(e);
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // if c.Value.ElementType == null then it is not a datatype like string, int, etc.
            // if it is int, string, double, float, char,
            if(c.Type.Name == typeof(int).Name)
            {
                SqlBuilder.Condition += c.Value.ToString() + " ";
            }
            else if (c.Type.Name == typeof(string).Name)
            {
                SqlBuilder.Condition += c.Value.ToString() + " ";
            }
            else if (c.Type.Name == typeof(double).Name)
            {
                SqlBuilder.Condition += c.Value.ToString() + " ";
            }
            else if (c.Type.Name == typeof(float).Name)
            {
                SqlBuilder.Condition += c.Value.ToString() + " ";
            }

            //if(c.Type != null && c.Type.IsGenericType && c.Type.Name == typeof(DemoLinq<>).Name && c.Type.GenericTypeArguments.Length == 1)
            //{
            //    SqlBuilder.SelectAll = c.Type.GenericTypeArguments[0].Name;
            //}
            // Console.WriteLine($"  Constant = {c.Value}");
            return base.VisitConstant(c);
        }        
    }
}
