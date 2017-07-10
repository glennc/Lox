using System;

namespace Lox
{
	public class Variable : Expr
	{
		public Variable (Token name)
		{
			this.name = name;
		}

		public Token name { get; set; }

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
