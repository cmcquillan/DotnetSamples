using System;

namespace OutParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            // This is a normal assignment.
            int i = 0;

            // Out parameters are another method of assigning variables.
            // They can assign existing variables.
            Int32.TryParse("32", out i);
            Console.WriteLine("Value = {0}", i);

            // They can also assign new variables.
            Int32.TryParse("42", out int n);
            Console.WriteLine("Value = {0}", n);

            // They can also be implicitly typed.
            Int32.TryParse("52", out var k);
            Console.WriteLine("Value = {0}", k);

            // They can also be discarded.
            Int32.TryParse("62", out var _);
            Console.WriteLine("Nothing to write!");

            // You can write your own methods with out parametes
            static void LetMeOut(out int hello)
            {
                // You cannot return without assiging the out parameter.
                hello = 7;
            }

            LetMeOut(out var seven);
            Console.WriteLine("Value = {0}", seven);

            new LetMeOutClass("25");
        }
    }

    class LetMeOutClass
    {
        // You can even assign fields with out parameters
        private readonly int _fieldAssign;

        public LetMeOutClass(string data)
        {
            // If you couldn't assign fields, you *may* have to write all this.
            Int32.TryParse(data, out var dataInt);

            if (dataInt == default)
                throw new ArgumentException("Invalid data", nameof(data));

            _fieldAssign = dataInt;

            // Instead, you can reduce this to the following.
            if (!Int32.TryParse(data, out _fieldAssign))
                throw new ArgumentException("Invalid data", nameof(data));

            Console.WriteLine(_fieldAssign);
        }
    }
}
