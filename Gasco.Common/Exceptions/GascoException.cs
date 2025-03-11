using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasco.Common.Exceptions
{
    public class GascoException : Exception
    {
        public int Code { get; set; }

        public GascoException(string message) : base(message) { }
        public GascoException(int code, string message) : base(message) { Code = code; }
        public GascoException(int code, string message, Exception innerException) : base(message, innerException) { Code = code; }
        public GascoException(ExceptionInfo exceptionInfo) : this(exceptionInfo.Code, exceptionInfo.Message) { }
        public GascoException(ExceptionInfo exceptionInfo, Exception innerException) : this(exceptionInfo.Code, exceptionInfo.Message, innerException) { }
    }
}
