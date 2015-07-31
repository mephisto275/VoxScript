using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    public class CallStatement : Statement
    {
        public string Identifier { get; }
        public ArgumentCallExpression ArgumentList { get; }

        public CallStatement(string identifier, ArgumentCallExpression arguments)
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
