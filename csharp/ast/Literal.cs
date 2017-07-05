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

		public override void Accept(IVisitor visitor)
		{
				visitor.Visit(this);
		}
	}
}
