using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Helpers
{
    public static class FileHelper
    {
        public static bool IsExcelExtension(string fileName)
        {
            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            return allowedExtensions.Contains(fileExtension);
        }

        public static decimal? ConvertStringToDecimal(string textAmount)
        {
            string amountWithoutDollarSign = System.Text.RegularExpressions.Regex.Replace(textAmount, @"[^0-9.]", "");

            var parseSucceeded = decimal.TryParse(amountWithoutDollarSign, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result);

            return parseSucceeded ? result : null;
        }

        public static byte[] ConvertTransactionsToCsv(List<Transaction> transactions)
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture));

            csvWriter.Context.RegisterClassMap<TransactionCsvMap>();
            csvWriter.WriteRecords(transactions);
            streamWriter.Flush();

            return memoryStream.ToArray();
        }
    }
}
