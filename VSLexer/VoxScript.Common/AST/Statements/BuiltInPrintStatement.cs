using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    class BuiltInPrintStatement : BuiltInStatement
    {
        public Expression Line { get; }

        public BuiltInPrintStatement(Expression line)
        {
            this.Line = line;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
