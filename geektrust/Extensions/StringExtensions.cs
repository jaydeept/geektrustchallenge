namespace geektrust.Extensions
{
    public static class StringExtensions
    {
        public static string[] GetSpaceSeparatedValues(this string inputValue)
        {
            return inputValue.Split(Constant.Space);
        }
    }
}
