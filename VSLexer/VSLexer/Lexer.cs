using System;
using System.Collections.Generic;
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
        multiplyTok,
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


        public Lexer()
        {

        }

        public void LoadScript(string input)
        {

        }

        public void MoveNext()
        {
            throw new NotImplementedException();
        }
    }
}
