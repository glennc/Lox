
using System;

namespace Lox
{
    public class Print : Stmt
    {
        public Expr Expr {get;set;}

        public Print(Expr expr)
        {
            this.Expr = expr;
        }

        public override void Accept(IStmtVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}