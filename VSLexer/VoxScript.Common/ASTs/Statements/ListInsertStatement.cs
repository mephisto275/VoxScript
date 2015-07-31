using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    public class ListInsertStatement : ListStatement
    {
        public Expression Name { get; }

        public ListInsertStatement(string identifier, Expression value, Expression name = null)
            : base(identifier, value)
        {
            this.Name = name;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
