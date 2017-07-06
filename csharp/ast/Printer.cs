using System;
using System.Text;

namespace Lox
{
    public class Printer : IVisitor<string>
    {

        public void Print(Expr expression)
        {
            Console.WriteLine(expression.Accept(this));
        }

        private string Parenthesize(string name, params Expr[] exprs)
        {
            var builder = new StringBuilder();
            builder.Append("(").Append(name);
            foreach(var expr in exprs)
            {
                builder.Append(" ");
                expr.Accept(this);
            }
            builder.Append(")");
            return builder.ToString();
        }

        public string Visit(Binary binary)
        {
            return Parenthesize(binary.op.Lexeme, binary.left, binary.right);
        }

        public string Visit(Grouping grouping)
        {
            return Parenthesize("group", grouping.exp);
        }

        public string Visit(Literal literal)
        {
            if(literal.val == null) return "nil";
            return literal.val.ToString();
        }

        public string Visit(Unary unary)
        {
            return Parenthesize(unary.op.Lexeme, unary.right);
        }
    }
}