using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    public class ListShiftStatement : ListStatement
    {
        public Expression Name { get; }

        public ListShiftStatement(string identifier, Expression value, Expression name = null)
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
