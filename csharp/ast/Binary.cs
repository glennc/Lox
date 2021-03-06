using System;

namespace Lox
{
	public class Binary : Expr
	{
		public Binary (Expr left, Token op, Expr right)
		{
			this.left = left;
			this.op = op;
			this.right = right;
		}

		public Expr left { get; set; }
		public  Token op { get; set; }
		public  Expr right { get; set; }

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
