using RedSharper.RedIL.Enums;

namespace RedSharper.RedIL
{
    class VariableDeclareNode : RedILNode
    {
        public string Name { get; set; }

        public ExpressionNode Value { get; set; }

        public VariableDeclareNode() : base(RedILNodeType.VariableDeclaration) { }

        public VariableDeclareNode(
            string name,
            ExpressionNode value)
            : base(RedILNodeType.VariableDeclaration)
        {
            Name = name;
            Value = value;
        }

        public override void AcceptVisitor<TState>(IRedILVisitor<TState> visitor, TState state)
        {
            visitor.VisitVariableDeclareNode(this, state);
        }
    }
}