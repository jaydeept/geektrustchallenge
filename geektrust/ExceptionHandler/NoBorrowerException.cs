using System;

namespace geektrust.ExceptionHandler
{
    public class NoBorrowerException : Exception
    {
        public NoBorrowerException(string borrowerName, string bankName) : base(
            $"Either the borrower '{borrowerName}' hasn't took the loan from bank '{bankName}' or has took more than one loan.")
        {
        }
    }
}
