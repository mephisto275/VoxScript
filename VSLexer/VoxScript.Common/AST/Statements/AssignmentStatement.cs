using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    public class AssignmentStatement : Statement
    {
        public string Identifier { get; }
        public Expression Value { get; }

        public AssignmentStatement(string identifier, Expression value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
