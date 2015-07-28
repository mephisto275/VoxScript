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
        longTok,
        doubleTok,
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
        andTok,
        orTok,
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
        Token MoveNext();
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

        private Dictionary<string, string> reservedWordToPhraseMap = new Dictionary<string, string>() {
            {"equal","^equal +to"},
            {"exit","^exit +loop"},
            {"divided","^divided by"},
            {"greater","^greater +than +or +equal +to|^greater +than"},
            {"less","^less +than +or +equal +to|^less +than"},
            {"element","^element +from"},
            {"type", "^type +of"},
            {"new","^new +list|^new +function"},
            {"exclusive","^exclusive +or"},
            {"read","^read +line"},
            {"mod","^mod +by"}
        };

        private Dictionary<string, Token> phraseToTokenMap = new Dictionary<string, Token>() { 
            {"if",Token.ifTok},
            {"set",Token.setTok},
            {"^equal +to",Token.assignTok},
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
            {"^exit +loop",Token.exitTok},
            {"return",Token.returnTok},
            {"plus", Token.plusTok},
            {"minus", Token.minusTok},
            {"times", Token.timesTok},
            {"multiplied",Token.timesTok},
            {"^divided +by",Token.divideTok},
            {"modulo",Token.moduloTok},
            {"^mod +by",Token.moduloTok},
            {"negative",Token.negativeTok},
            {"negate",Token.negativeTok},
            {"true",Token.boolTok},
            {"false",Token.boolTok},
            {"is",Token.isTok},
            {"not",Token.notTok},
            {"^greater +than",Token.greaterTok},
            {"^less +than",Token.lessTok},
            {"^greater +than +or +equal +to",Token.greaterThanOrEqualsTok},
            {"^less +than +or +equal +to",Token.lessThanOrEqualsTok},
            {"^exclusive +or",Token.exclusiveOrTok},
            {"or",Token.orTok},
            {"and",Token.andTok},
            {"append",Token.appendTok},
            {"^new +function",Token.newFuncTok},
            {"with",Token.withTok},
            {"^new +list",Token.newListTok},
            {"push",Token.pushTok},
            {"shift",Token.shiftTok},
            {"insert",Token.insertTok},
            {"remove",Token.removeTok},
            {"pop",Token.popTok},
            {"unshift",Token.unshiftTok},
            {"length",Token.lengthTok},
            {"^element +from",Token.elementTok},
            {"at",Token.atTok},
            {"print",Token.printTok},
            {"^read +line",Token.readLineTok},
            {"^type +of",Token.typeOfTok},
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

        public Token MoveNext()
        {
            Current = Next;
            Identifier = nextIdentifier;
            Value = nextValue;
            if (Current == Token.eofTok || Current == Token.INVALID)
            {
                Next = Token.INVALID;
                return Current;
            }
            if (currentLine == null)
            {
                Next = Token.eofTok;
                return Current;
            }

            HashSet<char> allowableCharactersAfterToken = new HashSet<char>() { ' ', ':', '.' };

            Char leadingChar = currentLine[currentPlace];
            if (Char.IsLetter(leadingChar))
            {
                
                Regex word = new Regex(@"^[a-z]+",RegexOptions.IgnoreCase);
                string remaining = currentLine.Substring(currentPlace);
                Match match = word.Match(remaining);

                bool isReserved;
                //if the word is a keyword, or it's the first part of a key phrase, mark this as an identifier.
                isReserved = (phraseToTokenMap.ContainsKey(match.Value)
                    || reservedWordToPhraseMap.ContainsKey(match.Value));
                if (isReserved)
                {
                    //It's a single reserved word.
                    if (phraseToTokenMap.ContainsKey(match.Value.ToLower()))
                    {
                        Next = phraseToTokenMap[match.Value.ToLower()];
                        //Grab boolean literals if they occur.
                        if (match.Value.ToLower() == "true")
                        {
                            Next = Token.boolTok;
                            nextValue = true;
                        }
                        if (match.Value.ToLower() == "false")
                        {
                            Next = Token.boolTok;
                            nextValue = false;
                        }
                        currentPlace += match.Value.Length;
                        if ((currentPlace + 1) < currentLine.Length && !allowableCharactersAfterToken.Contains(currentLine[currentPlace]))
                        {
                            Next = Token.INVALID;
                        }
                    }
                    else
                    {
                        string[] phrases = reservedWordToPhraseMap[match.Value.ToLower()].Split('|');
                        bool matched = false;
                        foreach (string phrase in phrases)
                        {
                            Regex phraseReg = new Regex(phrase, RegexOptions.IgnoreCase);
                            Match phraseMatch = phraseReg.Match(remaining);
                            if (phraseMatch.Success)
                            {
                                Next = phraseToTokenMap[phrase];
                                currentPlace += phraseMatch.Value.Length;
                                matched = true;
                                break;
                            }
                        }
                        //If we can't match the whole phrase. It's an error.
                        if (!matched)
                        {
                            Next = Token.INVALID;
                        }
                    }
                }
                else
                {
                    //It's an identifier. Parse forward until we get to a reserved word.
                    List<string> keywords = new List<string>();
                    keywords.Add(match.Value);
                    currentPlace += match.Value.Length;
                    bool invalid = false;
                    while (true)
                    {
                        if (currentPlace < currentLine.Length && !allowableCharactersAfterToken.Contains(currentLine[currentPlace]))
                        {
                            // The word isn't separated by a space, or followed by a : or .
                            invalid = true;
                            break;
                        }
                        //Trim out all the white space between words.
                        while (currentPlace < currentLine.Length && currentLine[currentPlace] == ' ')
                        {
                            currentPlace++;
                        }
                        //If we got to the end of the line, break;
                        if (currentPlace >= currentLine.Length)
                        {
                            break;
                        }
                        //If there is a non-alpha character, we're done.
                        if (!Char.IsLetter(currentLine[currentPlace]))
                        {
                            break;
                        }
                        remaining = currentLine.Substring(currentPlace);
                        match = word.Match(remaining);
                        //If the next word is a reserved word, break.
                        if ((phraseToTokenMap.ContainsKey(match.Value) || reservedWordToPhraseMap.ContainsKey(match.Value)))
                        {
                            //It's a reserved word. Break;
                            break;
                        }
                        else
                        {
                            keywords.Add(match.Value.ToLower());
                            currentPlace += match.Value.Length;
                        }
                    }
                    if (!invalid)
                    {
                        StringBuilder identifier = new StringBuilder();
                        keywords.Aggregate(identifier, (acc, keyword) =>
                        {
                            acc.Append("_");
                            acc.Append(keyword);
                            return acc;
                        });
                        Next = Token.identifierTok;
                        nextIdentifier = identifier.ToString();
                    }
                    else
                    {
                        Next = Token.INVALID;
                    }
                }
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
                    Next = Token.longTok;
                    nextValue = possibleLong;
                    currentPlace += match.Value.Length;
                }
                else if (double.TryParse(match.Value, out possibleDouble))
                {
                    Next = Token.doubleTok;
                    nextValue = possibleDouble;
                    currentPlace += match.Value.Length;
                }
                else
                {
                    Next = Token.INVALID;
                }
                if (currentPlace < currentLine.Length && !allowableCharactersAfterToken.Contains(currentLine[currentPlace]))
                {
                    //We don't have a space or anything following this. That's an error.
                    Next = Token.INVALID;
                }
            }
            else if (leadingChar == '"')
            {
                //If it's a quote, we need to grab everything to the next quote.
                currentPlace++;
                int startingPoint = currentPlace;
                while (currentPlace < currentLine.Length && currentLine[currentPlace] != '"')
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
                    currentPlace++;
                    if (currentPlace < currentLine.Length && !allowableCharactersAfterToken.Contains(currentLine[currentPlace]))
                    {
                        //We don't have a space or anything following this. That's an error.
                        Next = Token.INVALID;
                    }
                }
            }
            else if (leadingChar == '\t')
            {
                //It's a tab. This should be the easiest case.
                Next = Token.tabTok;
                currentPlace++;
            }
            else if (leadingChar == ':')
            {
                Next = Token.colonTok;
                currentPlace++;
            }
            else if (leadingChar == '.')
            {
                Next = Token.periodTok;
                currentPlace++;
            }
            else
            {
                Next = Token.INVALID;
            }

            //Trim off white space until we get to the next token:
            while (currentPlace < currentLine.Length && currentLine[currentPlace] == ' ')
            {
                currentPlace++;
            }

            if (currentPlace >= currentLine.Length)
            {
                currentLine = lineReader.ReadLine();
            }

            return Current;
        }
    }
}
