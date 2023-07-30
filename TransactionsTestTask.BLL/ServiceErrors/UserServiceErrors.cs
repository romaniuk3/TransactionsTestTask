using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsTestTask.BLL.ServiceErrors
{
    public class UserServiceErrors
    {
        public static List<KeyValuePair<string, string>> USER_NOT_FOUND_BY_USERNAME = new()
        {
            new("Get user by username failed", "User with given username was not found.")
        };
    }
}
