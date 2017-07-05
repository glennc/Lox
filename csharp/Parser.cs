using System;
using System.Collections.Generic;

namespace Lox
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _current;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _current = 0;    
        }

        /*expression → equality*/
        private Expr Expression()
        {
            return Equality();
        }

        //equality → comparison ( ( "!=" | "==" ) comparison )*
        private Expr Equality()
        {
            Expr expr = Comparison();

            while(Match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL))
            {
                Token op = Previous();
                Expr right = Expression();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        //comparison → term ( ( ">" | ">=" | "<" | "<=" ) term )*
        private Expr Comparison()
        {
            Expr expr = Term();

            while(Match(TokenType.GREATER, TokenType.GREATER_EQUAL,
                        TokenType.LESS, TokenType.LESS_EQUAL))
            {
                Token op = Previous();
                Expr right = Term();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        //term → factor ( ( "-" | "+" ) factor )*
        private Expr Term()
        {
            Expr expr = Factor();
            
            while(Match(TokenType.MINUS, TokenType.PLUS))
            {
                Token op = Previous();
                Expr right = Term();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        //factor → unary ( ( "/" | "*" ) unary )*
        private Expr Factor()
        {
            Expr expr = Unary();
            
            while(Match(TokenType.SLASH, TokenType.STAR))
            {
                Token op = Previous();
                Expr right = Unary();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        //unary      → ( "!" | "-" ) unary | primary
        public Expr Unary()
        {
            if(Match(TokenType.BANG, TokenType.MINUS))
            {
                return new Unary(Previous(), Unary());
            }

            return Primary();
        }

        //primary    → NUMBER | STRING | "false" | "true" | "nil" | "(" expression ")"
        private Expr Primary()
        {
            if(Match(TokenType.FALSE)) return new Literal(false);
            if(Match(TokenType.TRUE)) return new Literal(true);
            if(Match(TokenType.NIL)) return new Literal(null);

            if(Match(TokenType.NUMBER, TokenType.STRING))
            {
                return new Literal(Previous().Literal);
            }

            if(Match(TokenType.LEFT_PAREN))
            {
                Expr expr = Expression();
                Consume(TokenType.RIGHT_PAREN, "Expect ')' after '('.");
                return new Grouping(expr);
            }

            throw Error(Peek(), "Expect expression.");
        }

        public Expr Parse()
        {
            try
            {
                return Expression();
            }
            catch
            {
                return null;
            }
        }

        private void Synchronize()
        {
            Advance();

            while (!IsAtEnd()) 
            {
                if (Previous().Type == TokenType.SEMICOLON) return;

                switch (Peek().Type) {
                    case TokenType.CLASS:
                    case TokenType.FUN:
                    case TokenType.VAR:
                    case TokenType.FOR:
                    case TokenType.IF:
                    case TokenType.WHILE:
                    case TokenType.PRINT:
                    case TokenType.RETURN:
                    return;
            }

            Advance();
            }
        }

        private Token Consume(TokenType type, string msg)
        {
            if(Check(type)) return Advance();
            throw Error(Peek(), msg);
        }

        private Exception Error(Token token, string msg)
        {
            Lox.Error(token, msg);
            return new InvalidOperationException(msg);
        }

        private bool Match(params TokenType[] tokens)
        {
            foreach(TokenType type in tokens)
            {
                if (Check(type)) {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) _current++;
            return Previous();
        }

        private Token Previous()
        {
            return _tokens[_current - 1];
        }

        private bool Check(TokenType tokenType)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == tokenType;
        }

        private Token Peek()
        {
            return _tokens[_current];
        }

        private bool IsAtEnd()
        {
            return Peek().Type == TokenType.EOF;
        }
    }
}