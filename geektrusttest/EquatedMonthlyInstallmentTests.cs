using System;
using System.Collections.Generic;
using System.Text;
using geektrust;
using NUnit.Framework;

namespace geektrusttest
{
    public class EquatedMonthlyInstallmentTests
    {
        [TestCase(2000, 2, 2, 87)]
        [TestCase(2000, 3, 2, 89)]
        public void GivenPrincipleRateAndYears_WhenGetEMI_ShouldReturnEMIAmount(int principleAmount, int rate, int noOfYears, int expectedEmiAmount)
        {
            // Given
            var equatedMonthlyInstallment = new EquatedMonthlyInstallment(principleAmount, rate, noOfYears);

            // When
            var emiAmount = equatedMonthlyInstallment.GetEquatedMonthlyInstallment();

            // Then
            Assert.AreEqual(expectedEmiAmount, emiAmount);
        }
    }
}
