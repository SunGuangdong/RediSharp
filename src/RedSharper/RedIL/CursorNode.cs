using RedSharper.RedIL.Enums;

namespace RedSharper.RedIL
{
    class CursorNode : ExpressionNode
    {
        public CursorNode()
            : base(RedILNodeType.Cursor, DataValueType.Unknown)
        { }

        public override TReturn AcceptVisitor<TReturn, TState>(IRedILVisitor<TReturn, TState> visitor, TState state)
            => visitor.VisitCursorNode(this, state);
    }
}