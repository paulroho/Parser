﻿namespace ParserTechPlayground
{
    // Assignment   : Identifier AssignOp Expression EOF
    public class Assignment
    {
        private Identifier _assignee;
        private INode _assigner;

        private Assignment(Identifier assignee, INode assigner)
        {
            _assignee = assignee;
            _assigner = assigner;
        }

        public Identifier Assignee
        {
            get { return _assignee; }
        }

        public INode Assigner
        {
            get { return _assigner; }
        }

        internal static Assignment Produce(TokenBuffer tokens)
        {
            var assignee = Identifier.Produce(tokens);
            if (assignee == null)
                //throw new ParseException("Assignment must start with a identifier for the assignee.");
                return null;

            var assignOp = tokens.GetTerminal<AssignmentOperator>();
            if (assignOp == null)
                //throw new ParseException("Expected assignment operator.");
                return null;

            var assigner = Expression.Produce(tokens);
            if (assigner == null)
                throw new ParseException("Expected expression for the right side of the assignment.");

            return new Assignment(assignee, assigner);
        }
    }
}
