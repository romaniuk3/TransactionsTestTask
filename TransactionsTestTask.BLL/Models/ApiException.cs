using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransactionsTestTask.BLL.Models
{
    public class ApiException : ApiError
    {
        public ApiException(int statusCode, string? message = null, string? details = null) : base(statusCode)
        {
            Errors.Add(new KeyValuePair<string, string>("Message", message ?? "Internal server error. Sorry something went wrong at the server side"));
            if (details != null)
            {
                Errors.Add(new KeyValuePair<string, string>("Details", message ?? details));
            }
        }
    }
}
