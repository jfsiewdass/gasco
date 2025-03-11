using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasco.Common.Exceptions
{
    public class ExceptionInfo
    {
        public ExceptionInfo(int code, string message) 
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
        public string GetMessageWith(params string[] param) => string.Format(Message, param);
    }

}
