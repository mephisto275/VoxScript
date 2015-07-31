using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxScript.Common.ASTs.Expressions
{
    public abstract class NumericBinaryExpression : NumericExpression
    {
        public Expression Left { get; }
        public Expression Right { get; }

        public NumericBinaryExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
