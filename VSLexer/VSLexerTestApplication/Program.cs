using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Lexer;

namespace VSLexerTestApplication
{
    class Program
    {
        private static Dictionary<Token,string> phraseToTokenMap = new Dictionary<Token,string>()
        {
            //Assignment
            {Token.setTok, "set"},
            {Token.assignTok, "equal to"},
            //If...Then...Otherwise
            {Token.ifTok, "if"},
            {Token.thenTok, "then"},
            {Token.otherwiseTok, "otherwise"},
            //Loop Statements
            {Token.loopTok, "loop"},
            {Token.whileTok, "while"},
            {Token.fromTok, "from"},
            {Token.toTok, "to"},
            {Token.byTok, "by"},
            {Token.overTok, "over"},
            {Token.intoTok, "into"},
            {Token.namedTok, "named"},
            {Token.nextTok, "next"},
            {Token.exitTok, "exit"},
            //Function calls
            {Token.callTok, "call"},
            {Token.returnTok, "return"},
            {Token.newFuncTok, "new function"},
            {Token.withTok, "with"},
            //Numeric Operations
            {Token.plusTok, "plus"},
            {Token.minusTok, "minus"},
            {Token.timesTok, "times"},
            {Token.divideTok, "divided by"},
            {Token.moduloTok, "modulo"},
            {Token.negativeTok, "negative"},
            //Boolean Operations
            {Token.isTok, "is" },
            {Token.notTok, "not"},
            {Token.greaterTok, "greater than"},
            {Token.lessTok, "less than"},
            {Token.greaterThanOrEqualsTok, "greater than or equal to"},
            {Token.lessThanOrEqualsTok, "less than or equal to"},
            {Token.exclusiveOrTok, "exclusive or"},
            {Token.andTok, "and"},
            {Token.orTok, "or"},
            //String Operations
            {Token.appendTok, "append"},
            //Lists
            {Token.newListTok, "new list"},
            {Token.pushTok, "push"},
            {Token.popTok, "pop"},
            {Token.ontoTok, "onto"},
            {Token.shiftTok, "shift"},
            {Token.unshiftTok, "unshift"},
            {Token.insertTok, "insert into"},
            {Token.removeTok, "remove from"},
            {Token.lengthTok, "length of"},
            {Token.elementTok, "element from"},
            {Token.atTok, "at"},
            //Type
            {Token.typeOfTok, "type of"},
            //IO
            {Token.printTok, "print"},
            {Token.readLineTok, "read line"},
            //Other control flow
            {Token.colonTok, ": [New Line]"},
            {Token.tabTok, "Tab"},
            {Token.eofTok, "EOF"},
            {Token.periodTok, ". [New Line]"},
            //Temp values.
            {Token.UNINITIALIZED, "UNINITIALIZED"},
            {Token.INVALID, "INVALID"}
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a vox script line.");
                string input = Console.ReadLine();
                Lexer lexer = new Lexer();
                lexer.LoadScript(input);

                while ((lexer.MoveNext() != Token.eofTok) && (lexer.Current != Token.INVALID))
                {
                    Token current = lexer.Current;
                    Console.Write(" [");
                    if (current == Token.boolTok)
                    {
                        Console.Write((bool)lexer.Value);
                    }
                    else if (current == Token.stringTok)
                    {
                        Console.Write("\"{0}\"",(string)lexer.Value);
                    }
                    else if (current == Token.longTok)
                    {
                        Console.Write("{0}", (long)lexer.Value);
                    }
                    else if (current == Token.doubleTok)
                    {
                        Console.Write("{0}", (double)lexer.Value);
                    }
                    else if (current == Token.identifierTok)
                    {
                        Console.Write(lexer.Identifier);
                    }
                    else if (current == Token.periodTok || current == Token.colonTok)
                    {
                        Console.WriteLine(phraseToTokenMap[current] + "]");
                        continue;
                    }
                    else
                    {
                        Console.Write(phraseToTokenMap[current]);
                    }
                    Console.Write("]");
                }
                Console.WriteLine();
                Console.WriteLine("finished");
            }
        }
    }
}
