using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.AST
{
    public abstract class AST
    {
        public virtual bool IsInLoop { get; }
        public virtual AST Parent { get; set; }
        public abstract string Translate(ITranslator translator);
    }
}
