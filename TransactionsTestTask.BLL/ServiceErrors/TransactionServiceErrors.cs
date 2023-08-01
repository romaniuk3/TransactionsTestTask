using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsTestTask.BLL.ServiceErrors
{
    public class TransactionServiceErrors
    {
        public static List<KeyValuePair<string, string>> INCORRECT_FILE_EXTENSION = new()
        {
            new("File processing failed", "Allowed file extensions are .xls and .xslx")
        };

        public static List<KeyValuePair<string, string>> TRANSACTION_NOT_FOUND = new()
        {
            new("Update operation failed", "Transaction with given id was not found.")
        };

        public static List<KeyValuePair<string, string>> INCORRECT_AMOUNT_VALUE = new()
        {
            new("File processing failed", "Incorrect amount value.")
        };

        public static List<KeyValuePair<string, string>> INVALID_ROWS_COUNT = new()
        {
            new("File processing failed", "File must contain at least 2 rows.")
        };

        public static List<KeyValuePair<string, string>> UPDATE_FAILED_INVALID_STATUS = new()
        {
            new("Update operation failed", "Unsupported transaction status passed.")
        };
    }
}
