using System;
using System.Collections.Generic;

namespace Lox
{
    class Scanner
    {
        private string _code;
        private List<Token> _tokens;
        private int _start = 0;
        private int _current = 0;
        private int _line = 1;

        private static IDictionary<string, TokenType> _reservedKeywords = new Dictionary<string,TokenType>() {
            {"and", TokenType.AND },
            {"class", TokenType.CLASS },
            {"else", TokenType.ELSE },
            {"false", TokenType.FALSE },
            {"for", TokenType.FOR },
            {"fun", TokenType.FUN },
            {"if", TokenType.IF },
            {"nil", TokenType.NIL },
            {"or", TokenType.OR },
            {"print", TokenType.PRINT },
            {"return", TokenType.RETURN },
            {"super", TokenType.SUPER },
            {"this", TokenType.THIS },
            {"true", TokenType.TRUE },
            {"var", TokenType.VAR },
            {"while", TokenType.WHILE },
        };

        public Scanner(string code)
        {
            _code = code;
            _tokens = new List<Token>();
            
        }

        public List<Token> ScanTokens()
        {
            while(!AtEnd())
            {
                _start = _current;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.EOF, string.Empty, null, _line));
            return _tokens;
        }

        private bool AtEnd()
        {
            return _current >= _code.Length;
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LEFT_PAREN);
                    break;
                case ')':
                    AddToken(TokenType.RIGHT_PAREN);
                    break;
                case '{':
                    AddToken(TokenType.LEFT_BRACE);
                    break;
                case '}':
                    AddToken(TokenType.RIGHT_BRACE);
                    break;
                case ',':
                    AddToken(TokenType.COMMA);
                    break;
                case '.':
                    AddToken(TokenType.DOT);
                    break;
                case '-':
                    AddToken(TokenType.MINUS);
                    break;
                case '+':
                    AddToken(TokenType.PLUS);
                    break;
                case ';':
                    AddToken(TokenType.SEMICOLON);
                    break;
                case '*':
                    AddToken(TokenType.STAR);
                    break;
                case '!':
                    AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                case '/':
                    if (Match('/'))
                    {
                        while (Peek() != '\n' && !AtEnd()) Advance();
                    }
                    else
                    {
                        AddToken(TokenType.SLASH);
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    _line++;
                    break;
                case '"':
                    EatString();
                    break;
                default:
                    if (Char.IsDigit(c))
                    {
                        EatNumber();
                    }
                    else if(Char.IsLetter(c))
                    {
                        EatIdentifier();
                    }
                    else
                    {
                        Lox.Error(_line, $"Unexpected character at {(_current - 1)}");
                    }
                    break;
            }
        }

        private void EatIdentifier()
        {
            while(Char.IsLetterOrDigit(Peek())) Advance();

            //TODO: This means we do substr twice, should fix that.
            var text = _code.SubStr(_start, _current);

            var type = _reservedKeywords.GetValueOrDefault(text.Trim());

            if(type == TokenType.UNKNOWN) type = TokenType.IDENTIFIER;

            AddToken(type);
        }

        private void EatNumber()
        {
            while(Char.IsDigit(Peek())) Advance();

            if(Peek() == '.' && Char.IsDigit(PeekNext()))
            {
                Advance();

                while(Char.IsDigit(Peek())) Advance();
            }

            AddToken(TokenType.NUMBER, Double.Parse(_code.SubStr(_start, _current)));
        }

        private void EatString()
        {
            while (Peek() != '"' && !AtEnd()) {
                if (Peek() == '\n') _line++;
                Advance();
            }

            if (AtEnd()) {
                Lox.Error(_line, "Unterminated string.");
                return;
            }

            Advance();

            var value = _code.SubStr(_start + 1, _current - 1);
            AddToken(TokenType.STRING, value);
        }

        private char Peek()
        {
            if(AtEnd()) return '\0';
            return _code[_current];
        }

        private char PeekNext(int amount = 1)
        {
            if(_current + amount >= _code.Length) return '\0';

            return _code[_current + amount];
        }

        private bool Match(char expected)
        {
            if(AtEnd()) return false;
            if(_code[_current] != expected) return false;

            _current++;
            return true;
        }

        private void AddToken(TokenType tokenType, Object literal = null)
        {
            var text = _code.SubStr(_start, _current);
            _tokens.Add(new Token(tokenType, text, literal, _line));
        }

        private char Advance()
        {
            _current++;
            return _code[_current - 1];
        }
    }

    public static class Extensions
    {
        public static string SubStr(this string src, int startIndex, int endIndex)
        {
            var length = (endIndex - 1) - startIndex + 1;
            return src.Substring(startIndex, length);
        }
    }
}