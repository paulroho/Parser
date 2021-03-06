﻿namespace ParserTechPlayground
{
    public class MulDiv : INode
    {
        private readonly INode _left;
        private readonly MulDivOp _operator;
        private readonly INode _right;

        public MulDiv(INode left, MulDivOp @operator, INode right)
        {
            _left = left;
            _operator = @operator;
            _right = right;
        }

        public INode Left { get { return _left; } }
        public MulDivOp Operator { get { return _operator; } }
        public INode Right { get { return _right; } }

        // ExpRoot (MulDivOp ExpRoot)*
        internal static INode Produce(TokenBuffer tokens)
        {
            var expRoot = ExpRoot.Produce(tokens);
            if (expRoot == null)
                return null;

            tokens.SavePosition();

            return BuildSubNodes(expRoot, tokens);
        }

        private static INode BuildSubNodes(INode lhs, TokenBuffer tokens)
        {
            var mulDivOp = MulDivOp.Produce(tokens);
            if (mulDivOp == null)
                return lhs;

            var rhs = ExpRoot.Produce(tokens);
            if (rhs == null)
            {
                tokens.RestorePosition();
                return lhs;
            }

            lhs = new MulDiv(lhs, mulDivOp, rhs);

            return BuildSubNodes(lhs, tokens);
        }

        public double Evaluate()
        {
            if (_operator.IsTimes)
                return _left.Evaluate() * _right.Evaluate();
            return _left.Evaluate() / _right.Evaluate();
        }
    }
}
