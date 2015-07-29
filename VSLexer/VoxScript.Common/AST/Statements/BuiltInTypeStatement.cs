using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    public class BuiltInTypeStatement : BuiltInStatement
    {
        public string Identifier { get; }

        public BuiltInTypeStatement(string identifier)
        {
            this.Identifier = identifier;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
