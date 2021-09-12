using System;
using System.Collections.Generic;
using System.Linq;
using geektrust.ExceptionHandler;
using geektrust.Extensions;

namespace geektrust
{
    public class Balance
    {
        private readonly string _bankName;
        private readonly string _borrowerName;
        private readonly int _emiNo;

        public Balance(string inputValue)
        {
            ValidateInput(inputValue);
            var spaceSeparatedValues = inputValue.GetSpaceSeparatedValues();
            _bankName = spaceSeparatedValues[(int) BalanceEnum.BankName];
            _borrowerName = spaceSeparatedValues[(int) BalanceEnum.BorrowerName];
            _emiNo = int.Parse(spaceSeparatedValues[(int) BalanceEnum.EmiNo]);
        }

        public string GetBalanceWithRemainingInstallments(IReadOnlyList<Loan> loans, IReadOnlyList<Payment> payments)
        {
            var loanOnBorrowerName = LoanOnBorrowerName(loans);
            var equatedMonthlyInstallment = new EquatedMonthlyInstallment(loanOnBorrowerName.Principal, loanOnBorrowerName.RateOfInterest, loanOnBorrowerName.NoOfYears);
            var monthlyEmiInstallment = equatedMonthlyInstallment.GetEquatedMonthlyInstallment();
            var lumpsumAmountPaid = payments.Any(HasBorrowerPaidLumpsumAmount) 
                ? LumpsumAmountPaidBeforeEmi(payments) 
                : Constant.NO_ADVANCED_PAYMENT;
            var calculatedBalance = GetBalance(monthlyEmiInstallment, lumpsumAmountPaid);
            var totalAmountToPay = equatedMonthlyInstallment.GetTotalAmountToPay();
            var totalBalance = calculatedBalance > totalAmountToPay ? totalAmountToPay : calculatedBalance;
            var remainingInstallments = GetRemainingInstallments(loanOnBorrowerName, monthlyEmiInstallment, lumpsumAmountPaid);

            if (remainingInstallments * monthlyEmiInstallment + totalBalance < totalAmountToPay)
            {
                remainingInstallments++;
            }

            return $"{_bankName} {_borrowerName} {totalBalance} {remainingInstallments}";
        }

        private Loan LoanOnBorrowerName(IEnumerable<Loan> loans)
        {
            Loan loanOnBorrowerName;
            try
            {
                loanOnBorrowerName = loans.Single(HasBorrowerTookLoan);
            }
            catch (InvalidOperationException)
            {
                throw new NoBorrowerException(_borrowerName, _bankName);
            }

            return loanOnBorrowerName;
        }

        private int GetRemainingInstallments(Loan loan, int monthlyEmiInstallment, int lumpsumAmountPaid)
        {
            float montlyInstallMent = monthlyEmiInstallment;
            var lumpsumPaidForMonths = (int)Math.Round(lumpsumAmountPaid / montlyInstallMent);
            return (loan.NoOfYears * Constant.MonthsInSingleYear - _emiNo) - lumpsumPaidForMonths;
        }

        private int GetBalance(int monthlyEmiInstallment, int lumpsumAmountPaid)
        {
            return (monthlyEmiInstallment * _emiNo) + lumpsumAmountPaid;
        }

        private int LumpsumAmountPaidBeforeEmi(IReadOnlyList<Payment> payments)
        {
            return payments.Where(HasBorrowerPaidLumpsumAmount).Where(paymentMadeByBorrower => paymentMadeByBorrower.EmiNo <= _emiNo).Sum(paymentMadeByBorrower => paymentMadeByBorrower.LumpsumAmount);
        }

        private bool HasBorrowerTookLoan(Loan loan)
        {
            return loan.BorrowerName.Equals(_borrowerName, StringComparison.InvariantCultureIgnoreCase) &&
                   loan.BankName.Equals(_bankName, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool HasBorrowerPaidLumpsumAmount(Payment payment)
        {
            return payment.BorrowerName.Equals(_borrowerName, StringComparison.InvariantCultureIgnoreCase) &&
                   payment.BankName.Equals(_bankName, StringComparison.InvariantCultureIgnoreCase);
        }

        private void ValidateInput(string inputValue)
        {
            var spaceSeparatedValues = inputValue.GetSpaceSeparatedValues();
            if (spaceSeparatedValues.Length != Enum.GetValues(typeof(BalanceEnum)).Length || !int.TryParse(spaceSeparatedValues[(int) BalanceEnum.EmiNo], out _))
            {
                throw new InvalidInputException(Constant.Balance, inputValue);
            }
        }
    }
}
