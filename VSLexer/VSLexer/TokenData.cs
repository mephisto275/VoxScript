using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxScript.Common
{
    public class TokenData
    {
        public Token Token;
        public string Identifier;
        public object Value;

        public TokenData(Lexer lexer)
        {
            this.Token = lexer.Current;
            this.Identifier = lexer.Identifier;
            this.Value = lexer.Value;
        }
    }
}
