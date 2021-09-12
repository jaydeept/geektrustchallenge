using System;
using geektrust.ExceptionHandler;
using geektrust.Extensions;

namespace geektrust
{
    public class Loan
    {
        public string BankName { get; }
        public string BorrowerName { get; }
        public int Principal { get; }
        public int NoOfYears { get; }
        public int RateOfInterest { get; }

        public Loan(string inputValue)
        {
            ValidateInput(inputValue);
            var spaceSeparatedValues = inputValue.GetSpaceSeparatedValues();
            BankName = spaceSeparatedValues[(int) LoanEnum.BankName];
            BorrowerName = spaceSeparatedValues[(int) LoanEnum.BorrowerName];
            Principal = int.Parse(spaceSeparatedValues[(int) LoanEnum.Principal]);
            NoOfYears = int.Parse(spaceSeparatedValues[(int) LoanEnum.NoOfYears]);
            RateOfInterest = int.Parse(spaceSeparatedValues[(int) LoanEnum.RateOfInterest]);
        }

        private void ValidateInput(string inputValue)
        {
            var spaceSeparatedValues = inputValue.GetSpaceSeparatedValues();

            if (spaceSeparatedValues.Length != Enum.GetValues(typeof(LoanEnum)).Length || !int.TryParse(spaceSeparatedValues[(int) LoanEnum.NoOfYears], out _) || !int.TryParse(spaceSeparatedValues[(int) LoanEnum.Principal], out _) || !int.TryParse(spaceSeparatedValues[(int) LoanEnum.RateOfInterest], out _))
            {
                throw new InvalidInputException(Constant.Loan, inputValue);
            }
        }
    }
}
