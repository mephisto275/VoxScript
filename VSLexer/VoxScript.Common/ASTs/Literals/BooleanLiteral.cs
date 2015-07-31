using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Literals
{
    public class BooleanLiteral : BooleanExpression
    {
        public bool Value { get; }

        public BooleanLiteral(bool value)
        {
            this.Value = value;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
