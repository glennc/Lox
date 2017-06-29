namespace Lox
{
    public class Token
    {
        TokenType Type {get;set;}
        string Lexeme {get;set;}
        object Literal {get;set;}
        int Line {get;set;}

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
        }

        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal}";
        }
    }
}