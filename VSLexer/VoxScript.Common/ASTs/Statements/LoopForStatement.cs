using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    public class LoopForStatement : LoopStatement
    {
        public string Identifier { get; }

        public NumericExpression Start { get; }
        public NumericExpression End { get; }
        public NumericExpression Step { get; }

        public LoopForStatement(string indentifier, NumericExpression start, NumericExpression end, NumericExpression step = null)
        {
            this.Identifier = indentifier;
            if(this.Identifier != null)
            {
                this.LoopBlock.Scope.AddToScope(this.Identifier);
            }

            this.Start = start;
            this.End = end;
            this.Step = step;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
