using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxScript.Common.AST
{
    public class Scope
    {
        private HashSet<string> variables = new HashSet<string>();
        public bool IsInScope(string variable)
        {
            return variables.Contains(variable);
        }

        public void AddToScope(string variable)
        {
            variables.Add(variable);
        }
    }
}
