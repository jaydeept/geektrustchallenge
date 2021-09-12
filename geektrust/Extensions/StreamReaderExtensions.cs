using System.IO;

namespace geektrust.Extensions
{
    public static class StreamReaderExtensions
    {
        public static string ReadTrimmedLine(this StreamReader streamReader)
        {
            return streamReader.ReadLine()?.TrimStart().TrimEnd();
        }
    }
}
