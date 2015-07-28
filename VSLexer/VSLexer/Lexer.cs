using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VoxScript.Lexer
{
    public enum Token
    {
        //identifiers and literals
        identifierTok = 0,
        boolTok,
        stringTok,
        numberTok,
        //Assignment
        setTok,
        assignTok,
        //If...Then...Otherwise
        ifTok,
        thenTok,
        otherwiseTok,
        //Loop Statements
        loopTok,
        whileTok,
        fromTok,
        toTok,
        byTok,
        overTok,
        intoTok,
        namedTok,
        nextTok,
        exitTok,
        //Function calls
        callTok,
        returnTok,
        newFuncTok,
        withTok,
        //Numeric Operations
        plusTok,
        minusTok,
        timesTok,
        divideTok,
        moduloTok,
        negativeTok,
        //Boolean Operations
        isTok,
        notTok,
        greaterTok,
        lessTok,
        greaterThanOrEqualsTok,
        lessThanOrEqualsTok,
        exclusiveOrTok,
        //String Operations
        appendTok,
        //Lists
        newListTok,
        pushTok,
        popTok,
        ontoTok,
        shiftTok,
        unshiftTok,
        insertTok,
        removeTok,
        lengthTok,
        elementTok,
        atTok,
        //Type
        typeOfTok,
        //IO
        printTok,
        readLineTok,
        //Other control flow
        colonTok,
        tabTok,
        eofTok,
        periodTok,
        //Temp values.
        UNINITIALIZED,
        INVALID
    }

    public interface ILexer
    {
        Token Current { get; }
        Token Next { get; }
        Object Value { get; }
        String Identifier { get; }
        void MoveNext();
        void LoadScript(string input);
    }

    public class Lexer : ILexer
    {
        public Token Current { get; private set; }
        public Token Next { get; private set; }
        public Object Value { get; private set; }
        public string Identifier { get; private set; }

        private string currentLine;
        private int currentPlace;
        private Object nextValue;
        private string nextIdentifier;
        private StringReader lineReader = new StringReader("");
        private Dictionary<string, Token> reservedWords = new Dictionary<string, Token>() { 
            {"if",Token.ifTok},
            {"set",Token.setTok},
            {"equal to",Token.assignTok},
            {"then",Token.thenTok},
            {"otherwise",Token.otherwiseTok},
            {"loop",Token.loopTok},
            {"while",Token.whileTok},
            {"from",Token.fromTok},
            {"to",Token.toTok},
            {"by",Token.byTok},
            {"over",Token.overTok},
            {"into",Token.intoTok},
            {"named",Token.namedTok},
            {"call",Token.callTok},
            {"next",Token.nextTok},
            {"exit loop",Token.exitTok},
            {"return",Token.returnTok},
            {"plus", Token.plusTok},
            {"minus", Token.minusTok},
            {"times", Token.timesTok},
            {"multiplied",Token.timesTok},
            {"divided by",Token.divideTok},
            {"modulo",Token.moduloTok},
            {"mod by",Token.moduloTok},
            {"negative",Token.negativeTok},
            {"negate",Token.negativeTok},
            {"true",Token.boolTok},
            {"false",Token.boolTok},
            {"is",Token.isTok},
            {"not",Token.notTok},
            {"greater than",Token.greaterTok},
            {"less than",Token.lessTok},
            {"greater than or equal to",Token.greaterThanOrEqualsTok},
            {"less than or equal to",Token.lessThanOrEqualsTok},
            {"exclusive or",Token.exclusiveOrTok},
            {"append",Token.appendTok},
            {"new function",Token.newFuncTok},
            {"with",Token.withTok},
            {"new list",Token.newListTok},
            {"push",Token.pushTok},
            {"shift",Token.shiftTok},
            {"insert",Token.insertTok},
            {"remove",Token.removeTok},
            {"pop",Token.popTok},
            {"unshift",Token.unshiftTok},
            {"length",Token.lengthTok},
            {"element from",Token.elementTok},
            {"at",Token.atTok},
            {"print",Token.printTok},
            {"read line",Token.readLineTok},
            {"type of",Token.typeOfTok},
            {"onto",Token.ontoTok}
        };

        public Lexer()
        {
        }

        public void LoadScript(string input)
        {
            lineReader = new StringReader(input);
            currentLine = lineReader.ReadLine();
            Current = Token.UNINITIALIZED;
            Next = Token.UNINITIALIZED;
            MoveNext();
        }

        public void MoveNext()
        {
            Current = Next;
            Identifier = nextIdentifier;
            Value = nextValue;
            if (Current == Token.eofTok || Current == Token.INVALID)
            {
                Next = Token.INVALID;
                return;
            }
            if (currentLine == null)
            {
                Next = Token.eofTok;
                return;
            }

            Char leadingChar = currentLine[currentPlace];
            if (Char.IsLetter(leadingChar))
            {
                Regex word = new Regex(@"\w+");


                string accumulator = "";
                string nextWord = "";
                //We need to parse either an identifier or a reserved word.
                do
                {
                    string remaining = currentLine.Substring(currentPlace);
                    Match match = word.Match(remaining);

                    accumulator += match.Value + " ";

                } while (currentPlace < currentLine.Length && Char.IsLetter(leadingChar) && !reservedWords.ContainsKey(accumulator));
            }
            else if (Char.IsDigit(leadingChar) || leadingChar == '-')
            {
                Regex number = new Regex(@"(-)?((\d*)\.\d+|(\d+)(\.\d+)?)");
                string remaining = currentLine.Substring(currentPlace);
                Match match = number.Match(remaining);
                long possibleLong;
                double possibleDouble;
                if (long.TryParse(match.Value, out possibleLong))
                {
                    Next = Token.numberTok;
                    nextValue = possibleLong;
                }
                else if (double.TryParse(match.Value, out possibleDouble))
                {
                    Next = Token.numberTok;
                    nextValue = possibleDouble;
                }
                else
                {
                    Next = Token.INVALID;
                }
            }
            else if (leadingChar == '"')
            {
                //If it's a quote, we need to grab everything to the next quote.
                int startingPoint = currentPlace + 1;
                while (currentPlace < currentLine.Length && currentLine[currentPlace] == '"')
                {
                    currentPlace++;
                }
                if (currentPlace == currentLine.Length)
                {
                    //We got to the end of the line without closing the quotes.
                    Next = Token.INVALID;
                }
                else
                {
                    nextValue = currentLine.Substring(startingPoint, currentPlace - startingPoint);
                    Next = Token.stringTok;
                }
            }
            else if (leadingChar == '\t')
            {
                //It's a tab. This should be the easiest case.
                Next = Token.tabTok;
            }
            else if (leadingChar == ':')
            {
                Next = Token.colonTok;
            }
            else if (leadingChar == '.')
            {
                Next = Token.periodTok;
            }
            else
            {
                Next = Token.INVALID;
            }

            //Move it off of the last character.
            currentPlace++;
            //Trim off white space until we get to the next token:
            while (currentPlace < currentLine.Length && currentLine[currentPlace] == ' ')
            {
                currentPlace++;
            }

            if (currentPlace == currentLine.Length)
            {
                currentLine = lineReader.ReadLine();
            }

        }
    }
}
