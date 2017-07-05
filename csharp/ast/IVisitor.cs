using System;

namespace Lox
{
	public interface IVisitor
	{
		void Visit(Binary binary);
		void Visit(Grouping grouping);
		void Visit(Literal literal);
		void Visit(Unary unary);
	}
}
