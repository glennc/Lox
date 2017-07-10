namespace Lox
{
    public interface IStmtVisitor
    {
        void Visit(Print print);
        void Visit(Expression expr);
        void Visit(Var var);
    }
}