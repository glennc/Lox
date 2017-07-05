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

		public override void Accept(IVisitor visitor)
		{
				visitor.Visit(this);
		}
	}
}
