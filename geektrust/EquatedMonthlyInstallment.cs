using System;

namespace geektrust
{
    public class EquatedMonthlyInstallment
    {
        private readonly int _principleAmount;
        private readonly int _noOfYears;

        private readonly Interest _interest;

        public EquatedMonthlyInstallment(int principleAmount, int rate, int noOfYears)
        {
            _principleAmount = principleAmount;
            _noOfYears = noOfYears;
            _interest = new Interest(rate, noOfYears, principleAmount);
        }

        public int GetEquatedMonthlyInstallment()
        {
            var calculatedInterest = _interest.GetInterest();
            float noOfMonths = _noOfYears * Constant.MonthsInSingleYear;
            return (int) Math.Ceiling((_principleAmount + calculatedInterest) / noOfMonths);
        }

        public int GetTotalAmountToPay()
        {
            return _interest.GetInterest() + _principleAmount;
        }
    }
}
