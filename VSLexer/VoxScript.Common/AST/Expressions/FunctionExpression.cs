using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Expressions
{
    public class FunctionExpression : Expression
    {
        public ArgumentDeclareExpression Arguments { get; }
        public Block BodyBlock { get; }

        public FunctionExpression(ArgumentDeclareExpression arguments, Block block)
        {
            this.Arguments = arguments;
            this.BodyBlock = block;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
