using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Literals
{
    public class NumericLiteral : NumericExpression
    {
        public object Value { get; }

        public NumericLiteral(long value)
        {
            this.Value = value;
        }

        public NumericLiteral(double value)
        {
            this.Value = value;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
