using System;
using System.Runtime.Serialization;

namespace Lox
{
    [Serializable]
    public class RuntimeError : Exception
    {
        public Token Token {get;set;}
        public string ErrorMessage {get; set;}

        public RuntimeError(Token op, string message) : base(message)
        {
            this.Token = op;
            this.ErrorMessage = message;
        }
    }
}