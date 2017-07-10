
namespace Lox
{
    public abstract class Stmt
    {
        public abstract void Accept(IStmtVisitor visitor);
    }
}