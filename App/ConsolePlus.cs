using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class ConsolePlus
    {
        public static void WriterLine(string value, ConsoleColor color = ConsoleColor.Cyan)
        {
            ScopeColorWriter(() => Console.WriteLine(value), color);
        }

        public static void Writer(string value, ConsoleColor color = ConsoleColor.Green)
        {
            ScopeColorWriter(() => Console.Write(value), color);
        }

        public static void WriterLineGreen(string value)
        {
            ScopeColorWriter(() => Console.WriteLine(value), ConsoleColor.Green);
        }

        public static void WriterGreen(string value)
        {
            ScopeColorWriter(() => Console.Write(value), ConsoleColor.Green);
        }

        public static void WriterLineCyan(string value)
        {
            ScopeColorWriter(() => Console.WriteLine(value), ConsoleColor.Cyan);
        }

        public static void WriterCyan(string value)
        {
            ScopeColorWriter(() => Console.Write(value), ConsoleColor.Cyan);
        }

        protected static void ScopeColorWriter(Action action, ConsoleColor color)
        {
            ConsoleColor defcolor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            action?.Invoke();
            Console.ForegroundColor = defcolor;
        }

        //ConsolePlus.WriterLine("xxx", (ConsoleColor)0);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)1);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)2);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)3);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)4);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)5);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)6);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)7);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)8);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)9);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)10);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)11);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)12);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)13);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)14);
        //ConsolePlus.WriterLine("xxx", (ConsoleColor)15);
    }
}
