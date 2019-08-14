using System.Linq;
using RedSharper.RedIL;
using RedSharper.RedIL.Attributes;
using RedSharper.RedIL.Enums;
using RedSharper.RedIL.Nodes;

namespace RedSharper.Contracts
{
    class OkStatusResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller, ExpressionNode[] arguments)
        {
            return new StatusNode(Status.Ok);
        }
    }

    class ErrorStatusResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller, ExpressionNode[] arguments)
        {
            return new StatusNode(Status.Error, arguments.FirstOrDefault());
        }
    }

    class StatusIsOkResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller,
            ExpressionNode[] arguments)
        {
            return new BinaryExpressionNode(
                DataValueType.Boolean,
                BinaryExpressionOperator.And,
                new BinaryExpressionNode(DataValueType.Boolean, BinaryExpressionOperator.NotEqual, caller,
                    new NilNode()),
                new BinaryExpressionNode(DataValueType.Boolean, BinaryExpressionOperator.NotEqual,
                    new TableKeyAccessNode(caller, new ConstantValueNode(DataValueType.String, "ok")), new NilNode()));
        }
    }

    class StatusMessageResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller,
            ExpressionNode[] arguments)
        {
            return new TableKeyAccessNode(caller, new ConstantValueNode(DataValueType.String, "err"));
        }
    }

    class SingleResultAsIntResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller, ExpressionNode[] arguments)
        {
            return new CastNode(DataValueType.Integer, caller);
        }
    }

    class SingleResultAsLongResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller, ExpressionNode[] arguments)
        {
            return new CastNode(DataValueType.Integer, caller);
        }
    }

    class SingleResultAsDoubleResolver : RedILResolver
    {
        public override ExpressionNode Resolve(IExpressionVisitor visitor, ExpressionNode caller, ExpressionNode[] arguments)
        {
            return new CastNode(DataValueType.Float, caller);
        }
    }
}