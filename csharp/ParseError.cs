using System;
using System.Runtime.Serialization;

namespace Lox
{
    [Serializable]
    internal class ParseError : Exception
    {
        public Token Token {get;set;}

        public ParseError(Token token, string message) : base(message)
        {
            Token = token;
        }
    }
}