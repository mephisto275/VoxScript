using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Expressions
{
    public class ListElementExpression : ListExpression
    {
        public string Identifier { get; }
        public Expression Name { get; }

        public ListElementExpression(string identifier, Expression name)
        {
            this.Identifier = identifier;
            this.Name = name;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
