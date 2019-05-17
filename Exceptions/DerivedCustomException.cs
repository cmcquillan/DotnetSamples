using System;

namespace Exceptions
{
    public class DerivedCustomException : CustomException
    {
        public DerivedCustomException(string message) : base(message)
        {

        }
    }
}