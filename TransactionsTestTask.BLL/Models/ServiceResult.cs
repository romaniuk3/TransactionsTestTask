using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsTestTask.BLL.Models
{
    public class ServiceResult
    {
        public List<KeyValuePair<string, string>>? Errors { get; }
        public bool Succeeded => Errors == null;

        public ServiceResult(List<KeyValuePair<string, string>>? errors = null)
        {
            Errors = errors;
        }
    }

    public class ServiceResult<T>
    {
        public T? Value { get; }
        public bool Succeeded => Value != null;
        public List<KeyValuePair<string, string>>? Errors { get; }

        public ServiceResult(List<KeyValuePair<string, string>>? errors = null)
        {
            Errors = errors;
        }

        public ServiceResult(T value)
        {
            Value = value;
        }
    }
}
