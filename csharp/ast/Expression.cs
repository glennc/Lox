

using System;

namespace Lox
{
    public class Expression : Stmt
    {
        public Expression(Expr expr)
        {
            this.Expr = expr;
        }

        public Expr Expr { get; internal set; }

        public override void Accept(IStmtVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}