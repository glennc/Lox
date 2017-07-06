
using System;

namespace Lox
{
    public class Interpreter : IVisitor<object>
    {
        public void Interpret(Expr expression)
        {
            try
            {
                var result = expression.Accept(this);
                Console.WriteLine($"{Stringify(result)}");
            }
            catch(RuntimeError ex)
            {
                Lox.RuntimeError(ex);
            }
        }

        private string Stringify(object result)
        {
            if (result == null) return "nil";
            return result.ToString();
        }

        public object Visit(Binary binary)
        {
            var left = Evaluate(binary.left);
            var right = Evaluate(binary.right);

            switch(binary.op.Type)
            {
                case TokenType.MINUS:
                    CheckNumber(binary.op, right);
                    return (double)left - (double)right;
                case TokenType.SLASH:
                    CheckNumber(binary.op, left, right);
                    return (double)left / (double)right;
                case TokenType.STAR:
                    CheckNumber(binary.op, left, right);
                    return (double)left * (double)right;                    
                case TokenType.PLUS:
                    if(left is double && right is double)
                    {
                        CheckNumber(binary.op, left, right);                        
                        return (double)left + (double)right;
                    }
                    else if (left is string && right is string)
                    {
                        return $"{left}{right}";
                    }
                    else
                    {
                        return null;
                    }
                case TokenType.GREATER:
                    CheckNumber(binary.op, left, right);
                    return (double)left > (double)right;                    
                case TokenType.GREATER_EQUAL:
                    CheckNumber(binary.op, left, right);
                    return (double)left >= (double)right;                    
                case TokenType.LESS:
                    CheckNumber(binary.op, left, right);
                    return (double)left < (double)right;                    
                case TokenType.LESS_EQUAL:
                    CheckNumber(binary.op, left, right);
                    return (double)left <= (double)right;                    
                case TokenType.EQUAL_EQUAL:
                    CheckNumber(binary.op, left, right);
                    return IsEqual(left, right);                    
                case TokenType.BANG_EQUAL:
                    CheckNumber(binary.op, left, right);
                    return !IsEqual(left, right);                    
            }

            return null;
        }

        private void CheckNumber(Token op, object operand)
        {
            if(operand is double) return;
            throw new RuntimeError(op, "Operand must be a number.");
        }
        
        private void CheckNumber(Token op, object left, object right)
        {
            if(left is double && right is double) return;
            throw new RuntimeError(op, "Operand must be a number.");
        }

        private bool IsEqual(object left, object right)
        {
            if(left == null && right == null) return true;
            if(left == null) return false;

            return left.Equals(right);
        }

        public object Visit(Grouping grouping)
        {
            return Evaluate(grouping.exp);
        }

        private object Evaluate(Expr exp)
        {
            return exp.Accept(this);
        }

        public object Visit(Literal literal)
        {
            return literal.val;
        }

        public object Visit(Unary unary)
        {
            object right = Evaluate(unary.right);

            switch(unary.op.Type)
            {
                case TokenType.MINUS:
                    return -((double)right);
                case TokenType.BANG:
                    return !IsTruthy(right);
            }

            return null;
        }

        private bool IsTruthy(object right)
        {
            if(right == null) return false;
            if(right is bool) return (bool)right;

            return true;
        }
    }
}