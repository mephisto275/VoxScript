using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    public class ConditionalStatement : Statement
    {
        public BooleanExpression Condition { get; }
        public Block TrueBlock { get; }
        public Block FalseBlock { get; }

        public ConditionalStatement(BooleanExpression condition, Block trueBlock, Block falseBlock = null)
        {
            this.Condition = condition;
            this.TrueBlock = trueBlock;
            this.FalseBlock = falseBlock;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
