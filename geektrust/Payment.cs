using System;
using geektrust.ExceptionHandler;
using geektrust.Extensions;

namespace geektrust
{
    public class Payment
    {
        public string BankName { get; }
        public string BorrowerName { get; }
        public int LumpsumAmount { get; }
        public int EmiNo { get; }

        public Payment(string inputValue)
        {
            ValidateInput(inputValue);
            var spaceSeparatedValues = inputValue.GetSpaceSeparatedValues();
            BankName = spaceSeparatedValues[(int) PaymentEnum.BankName];
            BorrowerName = spaceSeparatedValues[(int) PaymentEnum.BorrowerName];
            LumpsumAmount = int.Parse(spaceSeparatedValues[(int) PaymentEnum.LumpsumAmount]);
            EmiNo = int.Parse(spaceSeparatedValues[(int) PaymentEnum.EmiNo]);
        }

        private void ValidateInput(string inputValue)
        {
            var spaceSeparatedValues = inputValue.GetSpaceSeparatedValues();

            if (spaceSeparatedValues.Length != Enum.GetValues(typeof(PaymentEnum)).Length || !int.TryParse(spaceSeparatedValues[(int) PaymentEnum.EmiNo], out _) || !int.TryParse(spaceSeparatedValues[(int) PaymentEnum.LumpsumAmount], out _))
            {
                throw new InvalidInputException(Constant.Payment, inputValue);
            }
        }
    }
}
