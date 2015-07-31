using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxScript.Common.ASTs;
using VoxScript.Common.ASTs.Expressions;
using VoxScript.Common.ASTs.Statements;

namespace VoxScript.Common
{
    public class Parser
    {
        private Lexer Lexer { get; }
        private ExitBlockException ExitBlock { get; set; }

        public Parser(Lexer lexer)
        {
            this.Lexer = lexer;
        }

        public Block ParseBlock(int depth, bool isInLoop, bool isInFunc, out bool eof)
        {
            Block block = new Block();

            eof = false;
            int lineDepth;

            while (true)
            {
                TokenData[] line = this.GetLine(out lineDepth);

                // check for invalid token
                if (line[0].Token == Token.INVALID)
                {
                    throw new Exception("Invalid Token found");
                }

                // check for eof
                if (line[0].Token == Token.eofTok)
                {
                    eof = true;
                    break;
                }

                // check for unexpected indent
                if (lineDepth > depth)
                {
                    throw new Exception("Unexpected indent");
                }

                // check for outdent (ie end of block)
                if (lineDepth < depth)
                {
                    int unwind = lineDepth - depth;

                    // we throw an exception which will be caught by the correct block
                    throw new ExitBlockException(block, unwind, line);
                }

            restartLoop:
                switch (line[0].Token)
                {
                    case Token.setTok:
                        AssignmentStatement assignmentStatemtnt = this.ParseAssignmentStatement(depth, line);
                        block.Scope.AddToScope(assignmentStatemtnt.Identifier);
                        block.AddStatement(assignmentStatemtnt);
                        break;

                    case Token.ifTok:
                        ConditionalStatement conditionalStatement = this.ParseConditionalStatement(depth, isInLoop, isInFunc, line);
                        block.AddStatement(conditionalStatement);

                        // terminate block if we are still unwinding
                        if (this.ExitBlock != null)
                        {
                            if (--this.ExitBlock.Unwind > 0)
                            {
                                return block;
                            }
                            else
                            {
                                // restart loop with exited block's line
                                line = this.ExitBlock.Line;
                                this.ExitBlock = null;
                                goto restartLoop;
                            }
                        }
                        break;

                    case Token.loopTok:
                        LoopStatement loopStatement = this.ParseLoopStatement(depth, isInLoop, isInFunc, line);
                        block.AddStatement(loopStatement);

                        // terminate block if we are still unwinding
                        if (this.ExitBlock != null)
                        {
                            if (--this.ExitBlock.Unwind > 0)
                            {
                                return block;
                            }
                            else
                            {
                                // restart loop with exited block's line
                                line = this.ExitBlock.Line;
                                this.ExitBlock = null;
                                goto restartLoop;
                            }
                        }
                        break;

                    case Token.callTok:
                        break;

                    // loop special statement
                    case Token.nextTok:
                        if (!isInLoop) throw new Exception("'next' not valid outside of a loop");
                        break;
                    case Token.exitTok:
                        if (!isInLoop) throw new Exception("'exit' not valid outside of a loop");
                        break;

                    // function special statement
                    case Token.returnTok:
                        if (!isInFunc) throw new Exception("'return' not valid outside of a function");
                        break;

                    // list statements
                    case Token.pushTok:
                        break;
                    case Token.shiftTok:
                        break;
                    case Token.insertTok:
                        break;
                    case Token.removeTok:
                        break;

                    // built in
                    case Token.printTok:
                        break;
                    case Token.typeOfTok:
                        break;
                }
            }

            return block;
        }

        private AssignmentStatement ParseAssignmentStatement(int depth, TokenData[] line)
        {
            int pos = depth + 1;
            string identifier;
            if (line[pos].Token != Token.identifierTok)
            {
                throw new Exception($"Exepecting identifier, got {line[pos].Token}");
            }
            identifier = line[pos].Identifier;

            pos++; //2
            if (line[pos].Token != Token.equalsTok)
            {
                throw new Exception($"Expecting 'equals to', got {line[pos].Token}");
            }

            if (line.Last().Token != Token.periodTok)
            {
                throw new Exception($"Expecting statement to end in a period");
            }

            pos++;
            Expression value = this.ParseExpression(line.Skip(pos).TakeWhile(t => t.Token != Token.periodTok));

            return new AssignmentStatement(identifier, value);
        }

        private ConditionalStatement ParseConditionalStatement(int depth, bool isInLoop, bool isInFunc, TokenData[] line)
        {
            int pos = depth + 1;
            TokenData[] last2 = line.Reverse().Take(2).Reverse().ToArray();

            if (last2[0].Token != Token.thenTok)
            {
                throw new Exception($"Expecting 'then', got {last2[0].Token}");
            }
            if (last2[1].Token != Token.colonTok)
            {
                throw new Exception($"Expecting statement to end in a colon");
            }

            Expression expression = this.ParseExpression(line.Skip(pos).Take(line.Length - 2 - pos));
            if (!(expression is BooleanExpression))
            {
                throw new Exception($"Expecting a boolean expression got {expression.GetType().Name}");
            }

            BooleanExpression booleanExpression = expression as BooleanExpression;

            Block trueBlock;
            Block falseBlock = null;
            bool eof;
            this.ExitBlock = null;
            try
            {
                trueBlock = this.ParseBlock(depth + 1, isInLoop, isInFunc, out eof);
            }
            catch (ExitBlockException exit)
            {
                trueBlock = exit.Block;
                if (trueBlock.Statements.Count == 0)
                {
                    throw new Exception("if block must contain at least 1 statement");
                }

                if (--exit.Unwind > 0)
                {
                    this.ExitBlock = exit;
                }
                else
                {
                    // otherwise?
                    if (exit.Line[depth].Token == Token.otherwiseTok)
                    {
                        if (exit.Line.Length == depth + 2 && exit.Line[depth + 1].Token == Token.colonTok)
                        {
                            throw new Exception("otherwise statement doesn't end in a colon");
                        }

                        try
                        {
                            falseBlock = this.ParseBlock(depth + 1, isInLoop, isInFunc, out eof);
                        }
                        catch (ExitBlockException exit2)
                        {
                            falseBlock = exit2.Block;
                            if (falseBlock.Statements.Count == 0)
                            {
                                throw new Exception("otherwise block must contain at least 1 statement");
                            }

                            if (--exit2.Unwind > 0)
                            {
                                this.ExitBlock = exit2;
                            }
                        }
                    }
                }
            }

            return new ConditionalStatement(booleanExpression, trueBlock, falseBlock);
        }

        private LoopStatement ParseLoopStatement(int depth, bool isInLoop, bool isInFunc, TokenData[] line)
        {

            if (line.Last().Token != Token.colonTok)
            {
                throw new Exception("loop statement doesn't end in a colon");
            }

            LoopStatement loopStatement = null;
            Expression expression;
            string list, value = null, named = null;
            int pos = depth + 1;

            switch (line[pos].Token)
            {
                case Token.whileTok:
                    pos++;
                    expression = this.ParseExpression(line.Skip(pos).TakeWhile(t => t.Token != Token.colonTok));

                    if (!(expression is BooleanExpression))
                    {
                        throw new Exception($"Expecting a boolean expression got {expression.GetType().Name}");
                    }

                    BooleanExpression booleanExpression = expression as BooleanExpression;

                    loopStatement = new LoopWhileStatement(booleanExpression);
                    break;
                case Token.overTok:
                    pos++; //2
                    if (line[pos].Token != Token.identifierTok)
                    {
                        throw new Exception($"loop expecting list identifier, got {line[pos].Token}");
                    }
                    list = line[pos].Identifier;

                    pos++; //3
                    if (line[pos].Token == Token.intoTok)
                    {
                        pos++; //4
                        if (line[pos].Token != Token.identifierTok)
                        {
                            throw new Exception($"loop expecting into identifier, got {line[pos].Token}");
                        }
                        value = line[pos].Identifier;

                        pos++; //5
                    }

                    if (line[pos].Token == Token.namedTok)
                    {
                        pos++; //4 or 6
                        if (line[pos].Token != Token.identifierTok)
                        {
                            throw new Exception($"loop expecting named identifier, got {line[pos].Token}");
                        }
                        named = line[pos].Identifier;
                    }

                    loopStatement = new LoopEachStatement(list, value, named);
                    break;
                case Token.fromTok:
                case Token.identifierTok:
                    pos++; //2
                    if(line[pos].Token == Token.identifierTok)
                    {
                        value = line[pos].Identifier;
                    }

                    pos++; //3
                    if(line[pos].Token != Token.fromTok)
                    {
                        throw new Exception($"Expecting 'from', got {line[pos].Token}");
                    }

                    NumericExpression from, to, step = null;

                    IEnumerable<TokenData> expTokens;
                    expTokens = line.Skip(pos).TakeWhile(t => t.Token != Token.toTok && t.Token != Token.colonTok);
                    pos += expTokens.Count() + 1;
                    if (line[pos].Token == Token.colonTok)
                    {
                        throw new Exception($"Expecting 'to' token in loop, but it was terminated by a colon");
                    }

                    expression = this.ParseExpression(expTokens);
                    if(!(expression is NumericExpression))
                    {
                        throw new Exception($"Expecting from numeric expression, got {expression.GetType().Name}");
                    }
                    from = expression as NumericExpression;

                    expTokens = line.Skip(pos).TakeWhile(t => t.Token != Token.withStepTok && t.Token != Token.colonTok);
                    pos += expTokens.Count() + 1;

                    expression = this.ParseExpression(expTokens);
                    if (!(expression is NumericExpression))
                    {
                        throw new Exception($"Expecting to numeric expression, got {expression.GetType().Name}");
                    }
                    to = expression as NumericExpression;

                    if(line[pos].Token == Token.withStepTok)
                    {
                        expTokens = line.Skip(pos).TakeWhile(t => t.Token != Token.colonTok);
                        expression = this.ParseExpression(expTokens);
                        if (!(expression is NumericExpression))
                        {
                            throw new Exception($"Expecting by numeric expression, got {expression.GetType().Name}");
                        }
                        step = expression as NumericExpression;
                    }

                    loopStatement = new LoopForStatement(value, from, to, step);
                    break;
            }

            Block loopBlock;
            bool eof;
            try
            {
                loopBlock = this.ParseBlock(depth + 1, true, isInFunc, out eof);
            }
            catch (ExitBlockException exit)
            {
                loopBlock = exit.Block;
                if (loopBlock.Statements.Count == 0)
                {
                    throw new Exception("if block must contain at least 1 statement");
                }

                if (--exit.Unwind > 0)
                {
                    this.ExitBlock = exit;
                }
            }

            loopStatement.LoopBlock = loopBlock;
            return loopStatement;
        }

        private Expression ParseExpression(IEnumerable<TokenData> tokens)
        {
            // check for value

            // check for identifier

            // check for not

            // check for negate

            // check for call

            // check for new

            // check for list expressions

            // check for readline
        }

        private TokenData[] GetLine(out int depth)
        {
            depth = 0;
            List<TokenData> line = new List<TokenData>();
            TokenData token;
            while (true)
            {
                this.Lexer.MoveNext();
                token = new TokenData(this.Lexer);

                if (token.Token == Token.tabTok)
                {
                    depth++;
                }
                else
                {
                    line.Add(token);
                }

                switch (token.Token)
                {
                    case Token.eofTok:
                    case Token.INVALID:
                    case Token.periodTok:
                    case Token.colonTok:
                        break;
                }
            }

            if (token.Token == Token.eofTok || token.Token == Token.INVALID)
            {
                return new TokenData[] { token };
            }
            else
            {
                return line.ToArray();
            }
        }
    }
}
