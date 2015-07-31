using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Expressions
{
    public class NumericMultiplyExpression : NumericBinaryExpression
    {
        public NumericMultiplyExpression(Expression left, Expression right)
            : base(left, right)
        {

        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
