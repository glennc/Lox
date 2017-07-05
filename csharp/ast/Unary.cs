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

		public override void Accept(IVisitor visitor)
		{
				visitor.Visit(this);
		}
	}
}
