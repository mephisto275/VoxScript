using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Expressions
{
    public class ListPopExpression : ListExpression
    {
        public string Identifier { get; }

        public ListPopExpression(string identifier)
        {
            this.Identifier = identifier;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
