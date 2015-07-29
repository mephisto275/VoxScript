using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Literals
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
