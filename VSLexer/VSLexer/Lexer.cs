using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        equalsTok,
        notTok,
        greaterTok,
        lessTok,
        greaterThanOrEqualsTok,
        lessThanOrEqualsTok,
        //String Operations
        appendTok,
        //Lists
        newListTok,
        pushTok,
        popTok,
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

        public Lexer()
        {

        }

        public void LoadScript(string input)
        {
            lineReader = new StringReader(input);
            currentLine = lineReader.ReadLine();
            Current = Token.INVALID;
            Next = Token.INVALID;
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
                //We need to parse either an identifier or a reserved word.
            }
            else if (Char.IsDigit(leadingChar) || leadingChar == '-')
            {
                //If it's a number, parse a literal.
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
