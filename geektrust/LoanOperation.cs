using System;
using System.Collections.Generic;
using System.Linq;
using geektrust.ExceptionHandler;

namespace geektrust
{
    public class LoanOperation
    {
        private readonly List<Loan> _loans;
        private readonly List<Payment> _payments;
        private readonly List<string> _balanceList;

        public LoanOperation()
        {
            _loans = new List<Loan>();
            _payments = new List<Payment>();
            _balanceList = new List<string>();
        }

        public void PerformOperation(string lineFromFile)
        {
            switch (lineFromFile.Split(Constant.Space).First())
            {
                case Constant.Loan:
                    PerformLoanOperation(lineFromFile);
                    break;
                case Constant.Payment:
                    PerformPaymentOperation(lineFromFile);
                    break;
                case Constant.Balance:
                    PerformBalanceOperation(lineFromFile);
                    break;
                default:
                    throw new Exception("Invalid line supplied in file");
            }
        }

        public IEnumerable<string> GetBalances()
        {
            return _balanceList;
        }

        private void PerformLoanOperation(string lineFromFile)
        {
            var loan = new Loan(lineFromFile.Substring(Constant.Loan.Length + 1));
            _loans.Add(loan);
        }

        private void PerformPaymentOperation(string lineFromFile)
        {
            var payment = new Payment(lineFromFile.Substring(Constant.Payment.Length + 1));
            _payments.Add(payment);
        }

        private void PerformBalanceOperation(string lineFromFile)
        {
            var balance = new Balance(lineFromFile.Substring(Constant.Balance.Length + 1));
            var balanceWithRemainingInstallments = balance.GetBalanceWithRemainingInstallments(_loans, _payments);
            _balanceList.Add(balanceWithRemainingInstallments);
        }
    }
}
