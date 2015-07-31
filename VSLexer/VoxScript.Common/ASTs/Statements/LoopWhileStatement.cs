using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    public class LoopWhileStatement : LoopStatement
    {
        public BooleanExpression Conditional { get; }

        public LoopWhileStatement(BooleanExpression conditional)
        {
            this.Conditional = conditional;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
