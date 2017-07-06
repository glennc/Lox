using System;

namespace Lox
{
	public class Unary : Expr
	{
		public Unary (Token op, Expr right)
		{
			this.op = op;
			this.right = right;
		}

		public Token op { get; set; }
		public  Expr right { get; set; }

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
