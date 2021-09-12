using System;
using System.Collections.Generic;
using System.IO;
using geektrust.Extensions;

namespace geektrust
{
    public class CalculateEquatedMonthlyInstallment
    {
        private static readonly LoanOperation LoanOperation = new LoanOperation();
        public static void Main(string[] args)
        {
            var fileName = args[0];
            var dataFromFile = GetDataFromFile(fileName);
            foreach (var line in dataFromFile)
            {
                LoanOperation.PerformOperation(line);
            }

            foreach (var balance in LoanOperation.GetBalances())
            {
                Console.WriteLine(balance);
            }
        }

        private static IEnumerable<string> GetDataFromFile(string fileName)
        {
            var dataFromFile = new List<string>();
            var fileStream = new FileStream(fileName, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                string line;
                while ((line = reader.ReadTrimmedLine()) != null)
                {
                    dataFromFile.Add(line);
                }
            }

            return dataFromFile;
        }
    }
}
