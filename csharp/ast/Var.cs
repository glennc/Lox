
using System;

namespace Lox
{
    public class Var : Stmt
    {
        public Token Name {get;set;}
        public Expr Expr {get;set;}
        public Var(Token name, Expr expr)
        {
            Name = name;
            Expr = expr;
        }

        public override void Accept(IStmtVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}