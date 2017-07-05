using System;
using System.Collections.Generic;
using System.IO;

namespace Lox
{
    public static class Lox
    {
        public static bool HadError = false;

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        public static void Error(Token token, string message)
        {
            if(token.Type == TokenType.EOF)
            {
                Report(token.Line, " at end ", message);                
            }
            else
            {
                Report(token.Line, $" at '{token.Lexeme}'", message);
            }
        }

        public static void Report(int line, string where, string message)
        {
            Console.WriteLine($"[line {line}] Error {where}: {message}");
            HadError = true;
        }
    }

    class Program
    {

        // static void Main(string[] args)
        // {
        //     Expr expression = new Binary(
        //         new Unary(
        //             new Token(TokenType.MINUS, "-", null, 1),
        //             new Literal(123)),
        //         new Token(TokenType.STAR, "*", null, 1),
        //         new Grouping(
        //             new Literal(45.67)));

        //     new Printer().Print(expression);
        // }

        static int Main(string[] args)
        {
            if(args.Length > 1)
            {
                Console.WriteLine("Usage: jlox [script]");
                return 0;
            }
            else if (args.Length == 1)
            {
                return RunFile(args[0]);
            }
            else
            {
                return RunPrompt();
            }
        }

        private static int RunFile(string codeFile)
        {
            return Run(File.ReadAllTextAsync(codeFile).Result);
        }

        private static int Run(string code)
        {
            var scanner = new Scanner(code);
            var tokens = scanner.ScanTokens();

            var parser = new Parser(tokens);

            var expr = parser.Parse();

            if(Lox.HadError) return 1;

            var printer = new Printer();
            printer.Print(expr);

            foreach(var token in tokens)
            {
                Console.WriteLine(token);
            }
            return Lox.HadError ? 0 : 1;
        }

        private static int RunPrompt()
        {
            while(true)
            {
                Lox.HadError = false;
                Console.Write("> ");
                Run(Console.ReadLine());
            }
        }
    }
}
