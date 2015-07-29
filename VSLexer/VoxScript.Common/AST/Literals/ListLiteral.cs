using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Literals
{
    public class ListLiteral : ListExpression
    {
        // need to add VoxList!
        public object Value { get; }

        public ListLiteral()
        {
            // need to replace with new VoxList()
            this.Value = new object();
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
