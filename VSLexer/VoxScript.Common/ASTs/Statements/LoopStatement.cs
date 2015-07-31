using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxScript.Common.ASTs.Statements
{
    public abstract class LoopStatement : Statement
    {
        public Block LoopBlock { get; set; }
    }
}
