using System;

namespace Lox
{
	public class Grouping : Expr
	{
		public Grouping (Expr exp)
		{
			this.exp = exp;
		}

		public Expr exp { get; set; }

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
