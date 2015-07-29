using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.AST.Statements;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST
{
    public class Block : AST
    {
        public List<Statement> Statements { get; } = new List<Statement>();
        public Scope Scope { get; } = new Scope();

        public Block()
        {

        }

        public void AddStatement(Statement statement)
        {
            this.Statements.Add(statement);
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
