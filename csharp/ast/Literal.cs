using System;

namespace Lox
{
	public class Literal : Expr
	{
		public Literal (object val)
		{
			this.val = val;
		}

		public object val { get; set; }

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
