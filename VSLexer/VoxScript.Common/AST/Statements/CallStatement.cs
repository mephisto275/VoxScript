using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    public class CallStatement : Statement
    {
        public string Identifier { get; }
        public ArgumentExpression ArgumentList { get; }

        public CallStatement(string identifier, ArgumentExpression arguments)
        {
            this.Identifier = identifier;
            this.ArgumentList = arguments;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
