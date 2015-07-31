using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.Translators;

namespace VoxScript.Common.ASTs.Statements
{
    public class LoopEachStatement : LoopStatement
    {
        public string ListIdentifier { get; }
        public string ValueIdentifier { get; }
        public string NameIdentifier { get; }

        public LoopEachStatement(string list, string value = null, string name = null)
        {
            this.ListIdentifier = list;
            this.ValueIdentifier = value;
            this.NameIdentifier = name;

            if(this.ValueIdentifier != null)
            {
                this.LoopBlock.Scope.AddToScope(this.ValueIdentifier);
            }
            if(this.NameIdentifier != null)
            {
                this.LoopBlock.Scope.AddToScope(this.NameIdentifier);
            }
        }

        public override string Translate(ITranslator translator)
        {
            throw new NotImplementedException();
        }
    }
}
