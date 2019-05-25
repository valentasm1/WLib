using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Data.Data.Extensions
{
    public sealed class EntityCastRemoverVisitor : ExpressionVisitor
    {
        public static Expression<Func<T, bool>> Convert<T>(
            Expression<Func<T, bool>> predicate)
        {
            var visitor = new EntityCastRemoverVisitor();

            var visitedExpression = visitor.Visit(predicate);

            return (Expression<Func<T, bool>>)visitedExpression;
        }

        public static Expression<Func<T, int?>> Convert<T>(Expression<Func<T, int?>> predicate)
        {
            var visitor = new EntityCastRemoverVisitor();

            var visitedExpression = visitor.Visit(predicate);

            return (Expression<Func<T, int?>>)visitedExpression;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert && (node.Type == typeof(IAuditable)))
            {
                return node.Operand;
            }

            return base.VisitUnary(node);
        }
    }
}
