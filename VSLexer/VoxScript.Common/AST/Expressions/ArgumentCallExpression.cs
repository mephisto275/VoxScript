using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Expressions
{
    public class ArgumentCallExpression : Expression
    {
        public List<string> Identifiers { get; } = new List<string>();

        public ArgumentCallExpression(IEnumerable<string> identifiers)
        {
            this.Identifiers.AddRange(identifiers);
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
