using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class PLCExcpetion : Exception
    {
        public Errcode ErrorCode { get; }

        public PLCExcpetion(Errcode errorCode) : this(errorCode, $"PLC communication failed with error '{errorCode}'.")
        {
        }

        public PLCExcpetion(Errcode errorCode, Exception innerException) : this(errorCode, innerException.Message,
            innerException)
        {
        }

        public PLCExcpetion(Errcode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public PLCExcpetion(Errcode errorCode, string message, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }
    }
}
