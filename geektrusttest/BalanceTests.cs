using System.Collections.Generic;
using System.Collections.ObjectModel;
using geektrust;
using geektrust.ExceptionHandler;
using NUnit.Framework;

namespace geektrusttest
{
    public class BalanceTests
    {
        [Test]
        public void GivenValidLoan_WhenGetBalanceWithRemainingInstallments_ShouldReturnCorrectValue()
        {
            // Given
            const string inputText = "IDIDI Dale 5";
            var loans = new List<Loan> {new Loan("IDIDI Dale 10000 5 4")};

            // When
            var balance = new Balance(inputText);
            var balanceWithRemainingInstallments = balance.GetBalanceWithRemainingInstallments(loans, new Collection<Payment>());

            // Then
            Assert.AreEqual("IDIDI Dale 1000 55", balanceWithRemainingInstallments);
        }

        [Test]
        public void GivenValidLoanAndPayment_WhenGetBalanceWithRemainingInstallments_ShouldReturnCorrectValue()
        {
            // Given
            const string inputText = "IDIDI Dale 6";
            var loans = new List<Loan> { new Loan("IDIDI Dale 5000 1 6") };
            var payments = new List<Payment> { new Payment("IDIDI Dale 1000 5") };

            // When
            var balance = new Balance(inputText);
            var balanceWithRemainingInstallments = balance.GetBalanceWithRemainingInstallments(loans, payments);

            // Then
            Assert.AreEqual("IDIDI Dale 3652 4", balanceWithRemainingInstallments);
        }

        [Test]
        public void GivenLoanDoesNotExists_WhenGetBalanceWithRemainingInstallments_ShouldThrowExcpetion()
        {
            // Given
            const string inputText = "IDIDI Dale 6";
            var loans = new List<Loan> { new Loan("IDIDI NotDale 5000 1 6") };

            // When
            var balance = new Balance(inputText);
            var exception = Assert.Throws<NoBorrowerException>(() => balance.GetBalanceWithRemainingInstallments(loans, new Collection<Payment>())) ;

            // Then
            Assert.AreEqual("Either the borrower 'Dale' hasn't took the loan from bank 'IDIDI' or has took more than one loan.", exception.Message);
        }

        [TestCase("IDIDI MissingEmi", "The value 'IDIDI MissingEmi' provided for 'BALANCE' is not valid.")]
        [TestCase("IDIDI Dale 5 ExtraValue", "The value 'IDIDI Dale 5 ExtraValue' provided for 'BALANCE' is not valid.")]
        [TestCase("IDIDI Dale InvalidEmi", "The value 'IDIDI Dale InvalidEmi' provided for 'BALANCE' is not valid.")]
        public void GivenInvalidValues_WhenBalanceObjectCreated_ShouldThrowInvalidInputException(string inputText, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidInputException>(() => _ = new Balance(inputText));
            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}
