using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Expressions;

namespace VoxScript.Common.AST.Statements
{
    public abstract class ListStatement : Statement
    {
        public string Identifier { get; }
        public Expression Value { get; }

        public ListStatement(string identifier, Expression value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }
    }
}
