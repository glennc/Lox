using System;
using System.Text;

namespace Lox
{
    public class Printer : IVisitor
    {
        private StringBuilder _builder;

        public Printer()
        {
            _builder = new StringBuilder();
        }

        public void Print(Expr expression)
        {
            expression.Accept(this);
            Console.WriteLine(_builder.ToString());
        }

        private void Parenthesize(string name, params Expr[] exprs)
        {
            _builder.Append("(").Append(name);
            foreach(var expr in exprs)
            {
                _builder.Append(" ");
                expr.Accept(this);
            }
            _builder.Append(")");
        }

        public void Visit(Binary binary)
        {
            Parenthesize(binary.op.Lexeme, binary.left, binary.right);
        }

        public void Visit(Grouping grouping)
        {
            Parenthesize("group", grouping.exp);
        }

        public void Visit(Literal literal)
        {
            if(literal.val == null) _builder.Append("nil");
            _builder.Append(literal.val);
        }

        public void Visit(Unary unary)
        {
            Parenthesize(unary.op.Lexeme, unary.right);
        }
    }
}