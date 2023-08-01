using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
