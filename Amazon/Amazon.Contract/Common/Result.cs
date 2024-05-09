using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Contract.Common
{
    public class Result<T>
    {
        public Result(int statusCode, List<string> message, T? data)
        {
            TracsId = Activity.Current.TraceId.ToString();
            StatusCode = statusCode;
            Message = message;
            this.data = data;
        }

        public string TracsId { get; set; }
        public int StatusCode { get; set; }
        public List<string> Message { get; set; }
        public T? data { get; set; }

    }
}
