using System.Collections.Generic;
using geektrust;
using NUnit.Framework;

namespace geektrusttest
{
    public class LoanOperationTests
    {
        [Test]
        public void GivenSampleDataOfLoan_WhenCheckedBalanceAfterPerformingLoanOperation_ShouldHaveCorrectValues()
        {
            // Given
            var loanOperation = new LoanOperation();
            GetSampleLoanDataFromAssignment().ForEach(sampleData => loanOperation.PerformOperation(sampleData));

            // When
            var balances = ToCommaSeparatedString(loanOperation.GetBalances());

            // Then
            Assert.AreEqual("IDIDI Dale 1326 9, IDIDI Dale 3652 4, UON Shelly 15856 3, MBI Harry 9044 10", balances);
        }

        private static List<string> GetSampleLoanDataFromAssignment()
        {
            var sampleLoanDataList = new List<string>
            {
                "LOAN IDIDI Dale 5000 1 6",
                "LOAN MBI Harry 10000 3 7",
                "LOAN UON Shelly 15000 2 9",
                "PAYMENT IDIDI Dale 1000 5",
                "PAYMENT MBI Harry 5000 10",
                "PAYMENT UON Shelly 7000 12",
                "BALANCE IDIDI Dale 3",
                "BALANCE IDIDI Dale 6",
                "BALANCE UON Shelly 12",
                "BALANCE MBI Harry 12"
            };

            return sampleLoanDataList;
        }

        private static string ToCommaSeparatedString(IEnumerable<string> values)
        {
            return string.Join(", ", values);
        }
    }
}
