using geektrust;
using NUnit.Framework;

namespace geektrusttest
{
    public class InterestTests
    {
        [TestCase(2, 2, 2000, 80)]
        [TestCase(10, 3, 150000, 45000)]
        public void GivenSampleRateNoOfYearsAndPrincipleAmount_WhenGetInterest_ShouldReturnExpectedValue(int rate, int noOfYears, int principleAmount, int expectedInterest)
        {
            // Given
            var interest = new Interest(rate, noOfYears, principleAmount);

            // When
            var calculatedInterest = interest.GetInterest();

            // Then
            Assert.AreEqual(expectedInterest, calculatedInterest);
        }
    }
}