
namespace Lox
{
    public abstract class Expr
    {
        public abstract void Accept(IVisitor visitor);
    }
}