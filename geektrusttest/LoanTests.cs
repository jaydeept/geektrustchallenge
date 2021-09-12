using geektrust;
using geektrust.ExceptionHandler;
using NUnit.Framework;

namespace geektrusttest
{
    public class LoanTests
    {
        [Test]
        public void GivenValidValuesForLoanInputText_WhenLoanIsCreated_ShouldMatchTheValues()
        {
            // Given
            const string inputText = "IDIDI Dale 10000 5 4";

            // When
            var loan = new Loan(inputText);

            // Then
            Assert.AreEqual("IDIDI", loan.BankName);
            Assert.AreEqual("Dale", loan.BorrowerName);
            Assert.AreEqual(10000, loan.Principal);
            Assert.AreEqual(5, loan.NoOfYears);
            Assert.AreEqual(4, loan.RateOfInterest);
        }

        [TestCase("IDIDI MissingRate 10000 5", "The value 'IDIDI MissingRate 10000 5' provided for 'LOAN' is not valid.")]
        [TestCase("IDIDI MissingRate 10000 5, 6, ExtraParameter", "The value 'IDIDI MissingRate 10000 5, 6, ExtraParameter' provided for 'LOAN' is not valid.")]
        [TestCase("IDIDI Dale 10000 5 InvalidRate", "The value 'IDIDI Dale 10000 5 InvalidRate' provided for 'LOAN' is not valid.")]
        public void GivenInvalidValues_WhenLoanObjectCreated_ShouldThrowInvalidInputException(string inputText, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidInputException>(() => _ = new Loan(inputText));
            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}
