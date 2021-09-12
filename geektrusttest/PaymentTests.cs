using geektrust;
using geektrust.ExceptionHandler;
using NUnit.Framework;

namespace geektrusttest
{
    public class PaymentTests
    {
        [Test]
        public void GivenValidValuesForPaymentInputText_WhenPaymentIsCreated_ShouldMatchTheValues()
        {
            // Given
            const string inputText = "IDIDI Dale 5000 5";

            // When
            var payment = new Payment(inputText);

            // Then
            Assert.AreEqual("IDIDI", payment.BankName);
            Assert.AreEqual("Dale", payment.BorrowerName);
            Assert.AreEqual(5000, payment.LumpsumAmount);
            Assert.AreEqual(5, payment.EmiNo);
        }

        [TestCase("IDIDI MissingEmi 5000", "The value 'IDIDI MissingEmi 5000' provided for 'PAYMENT' is not valid.")]
        [TestCase("IDIDI Dale 5000 5 ExtraParameter", "The value 'IDIDI Dale 5000 5 ExtraParameter' provided for 'PAYMENT' is not valid.")]
        [TestCase("IDIDI Dale 10000 InvalidEmi", "The value 'IDIDI Dale 10000 InvalidEmi' provided for 'PAYMENT' is not valid.")]
        public void GivenInvalidValues_WhenPaymentObjectCreated_ShouldThrowInvalidInputException(string inputText, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidInputException>(() => _ = new Payment(inputText));
            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}
