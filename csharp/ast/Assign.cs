using System;

namespace Lox
{
	public class Assign : Expr
	{
		public Assign (Token name, Expr value)
		{
			this.name = name;
			this.value = value;
		}

		public Token name { get; set; }
		public  Expr value { get; set; }

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
