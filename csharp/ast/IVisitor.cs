using System;

namespace Lox
{
	public interface IVisitor<T>
	{
		 T Visit(Binary binary);
		 T Visit(Grouping grouping);
		 T Visit(Literal literal);
		 T Visit(Unary unary);
		 T Visit(Variable variable);
		 T Visit(Assign assign);
	}
}
