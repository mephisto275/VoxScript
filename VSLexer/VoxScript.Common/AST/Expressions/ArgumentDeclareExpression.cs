using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Expressions
{
    class ArgumentDeclareExpression : Expression
    {
        public List<Expression> Values { get; } = new List<Expression>();

        public ArgumentDeclareExpression(IEnumerable<Expression> values)
        {
            this.Values.AddRange(values);
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
