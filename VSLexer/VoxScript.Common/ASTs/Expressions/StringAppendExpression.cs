using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Expressions
{
    class StringAppendExpression : StringExpression
    {
        public Expression Left { get; }
        public Expression Right { get; }

        public StringAppendExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
