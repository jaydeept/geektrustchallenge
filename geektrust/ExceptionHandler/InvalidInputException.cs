using System;

namespace geektrust.ExceptionHandler
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string type, string value) : base($"The value '{value}' provided for '{type}' is not valid.")
        {
        }
    }
}
