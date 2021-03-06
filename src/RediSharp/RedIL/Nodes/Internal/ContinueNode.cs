using RediSharp.RedIL.Enums;

namespace RediSharp.RedIL.Nodes.Internal
{
    class ContinueNode : RedILNode
    {
        public ContinueNode() 
            : base(RedILNodeType.Continue)
        {
        }

        public override TReturn AcceptVisitor<TReturn, TState>(IRedILVisitor<TReturn, TState> visitor, TState state)
        {
            // We don't want to visit continue nodes outside of RedIL's internal scope
            throw new System.NotImplementedException();
        }
    }
}