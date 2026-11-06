using System;
using System.IO;

namespace Laba10.Tests
{
    internal static class ConsoleHelper
    {
        public static string CaptureOutput(Action action)
        {
            TextWriter old = Console.Out;
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            try { action(); }
            finally { Console.SetOut(old); }
            return sw.ToString();
        }

        public static void WithInput(string input, Action action)
        {
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter());
            try { action(); }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }
        }
    }
}