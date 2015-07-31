using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs;

namespace VoxScript.Common
{
    public class ExitBlockException : Exception
    {
        public Block Block;
        public int Unwind;
        public TokenData[] Line;

        public ExitBlockException(Block block, int unwind, TokenData[] line)
            : base()
        {
            this.Block = block;
            this.Unwind = unwind;
            this.Line = line;
        }
    }
}
