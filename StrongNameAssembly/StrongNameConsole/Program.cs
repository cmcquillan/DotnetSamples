using System;
using StrongNameLibrary;

namespace StrongNameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var strongNameClass = new StrongNameClass();
            strongNameClass.WriteName();
        }
    }
}
