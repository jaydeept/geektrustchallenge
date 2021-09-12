using System;

namespace geektrust
{
    public class Interest
    {
        private readonly int _rate;
        private readonly int _noOfYears;
        private readonly int _principleAmount;

        public Interest(int rate, int noOfYears, int principleAmount)
        {
            _rate = rate;
            _noOfYears = noOfYears;
            _principleAmount = principleAmount;
        }

        public int GetInterest()
        {
            return  (int)Math.Ceiling((double) (((_rate * _principleAmount) / 100 ) * _noOfYears));
        }
    }
}
