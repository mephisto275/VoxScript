using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    class ListRemoveStatement : ListStatement
    {
        public Expression Name { get; }

        public ListRemoveStatement(string identifier, Expression name)
            : base(identifier, null)
        {
            this.Name = name;
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
