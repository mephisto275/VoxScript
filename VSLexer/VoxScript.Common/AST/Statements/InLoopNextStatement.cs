using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST.Statements
{
    public class InLoopNextStatement : InLoopStatement
    {
        public InLoopNextStatement(LoopStatement parent)
            : base(parent)
        {

        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
