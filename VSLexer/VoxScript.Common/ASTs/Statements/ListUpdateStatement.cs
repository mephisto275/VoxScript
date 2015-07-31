using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    class ListUpdateStatement : ListStatement
    {
        public Expression Name { get; }

        public ListUpdateStatement(string identifier, Expression value, Expression name)
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
