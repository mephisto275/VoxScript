using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxScript.Common.ASTs.Statements
{
    public abstract class InLoopStatement : Statement
    {
        public LoopStatement ParentLoop { get; }

        public InLoopStatement(LoopStatement parent)
        {
            this.Parent = parent;
        }
    }
}
