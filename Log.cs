using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit
{
    internal static class Log
    {
        public static void Error(string fmt, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fmt, args);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Info(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
        }
    }
}
