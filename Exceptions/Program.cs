using System;
using System.IO;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TestThrowBasicException();
            }
            // You can catch exception by type.
            catch (CustomException ex)
            {
                Console.WriteLine($"Caught {nameof(CustomException)}");
                Console.WriteLine(ex.ToString());
            }

            try
            {
                TestThrowBasicException();
            }
            // You can catch a base class as well.
            catch (Exception ex)
            {
                Console.WriteLine($"Caught {nameof(Exception)}");
                Console.WriteLine(ex.ToString());
            }

            try
            {
                TestThrowBasicException();
            }
            // You can chain catch blocks to catch several types.
            catch (InvalidOperationException)
            {
                Console.WriteLine("You should not see this");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught in second catch block.");
                Console.WriteLine(ex.ToString());
            }

            try
            {
                TestThrowBasicException();
            }
            // You can also use filters
            catch (CustomException ex)
            when (ex.Message.Contains("Problem"))
            {
                Console.WriteLine($"Caught {nameof(CustomException)} with filter");
                Console.WriteLine(ex.ToString());
            }

            try
            {
                TestThrowBasicException();
            }
            // Filter is not passed but subsequent exception is caught.
            catch (CustomException ex1)
            when (ex1 is IndexOutOfRangeException)
            {
                Console.WriteLine("You should not see this.");
            }
            catch (Exception ex2)
            {
                Console.WriteLine("Caught in subsequent block after bypassed filter.");
                Console.WriteLine(ex2.ToString());
            }

            // You can use finally {} to ensure that you can 
            // clean up after yourself if you allocate resources
            // in the try {} block.
            Stream resource = null;
            try
            {
                resource = new MemoryStream();
                TestThrowBasicException();

                Console.WriteLine("This will not get called.");
                resource?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception with finally block.");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Clean up our resource.
                Console.WriteLine("Executing finally {} cleanup.");
                resource?.Dispose();
                resource = null;
            }

            try
            {
                TestWithReferencedRethrow();
            }
            // You can re-throw exceptions
            // This is an example of 'throw ex;'
            catch (CustomException ex)
            {
                Console.WriteLine("Caught a re-throw of the exception reference.");
                Console.WriteLine(ex.ToString());
            }

            try
            {
                TestWithEmptyRethrow();
            }
            // You can re-throw exceptions
            // This is an example of 'throw;'
            // Note the difference in stack trace from the previous example.
            // Just using 'throw' preserves the previous stack trace.
            catch (CustomException ex)
            {
                Console.WriteLine("Caught a re-throw with an empty throw statement.");
                Console.WriteLine(ex.ToString());
            }
        }

        private static void TestThrowBasicException()
        {
            // Throw an exception
            throw new CustomException("Problem was found.");
        }

        private static void TestWithReferencedRethrow()
        {
            try
            {
                TestThrowBasicException();
            }
            catch (CustomException ex)
            {
                throw ex;
            }
        }

        private static void TestWithEmptyRethrow()
        {
            try
            {
                TestThrowBasicException();
            }
            catch (CustomException ex)
            {
                throw;
            }
        }
    }
}
