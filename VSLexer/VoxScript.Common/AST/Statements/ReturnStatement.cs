using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression Value { get; }

        public ReturnStatement(Expression value = null)
        {
            this.Value = value;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
