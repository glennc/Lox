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

        public static void Report(int line, string where, string message)
        {
            Console.WriteLine($"[line {line}] Error {where}: {message}");
            HadError = true;
        }
    }

    class Program
    {

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
                Console.WriteLine("> ");
                Run(Console.ReadLine());
            }
        }
    }
}
