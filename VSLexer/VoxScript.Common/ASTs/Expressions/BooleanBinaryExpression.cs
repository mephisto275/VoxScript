using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxScript.Common.ASTs.Expressions
{
    public abstract class BooleanBinaryExpression : BooleanExpression
    {
        public Expression Left { get; }
        public Expression Right { get; }

        public BooleanBinaryExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
