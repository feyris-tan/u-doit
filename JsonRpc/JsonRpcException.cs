using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace u_doit.JsonRpc
{
    [Serializable]
    public class JsonRpcException : Exception
    {
        public JsonRpcException()
        {
            Error = new Error();
            Error.code = 0;
            Error.message = null;
        }

        public JsonRpcException(string message) : base(message)
        {
            Error = new Error();
            Error.code = 0;
            Error.message = message;
        }

        public JsonRpcException(string message, Exception inner) : base(message, inner)
        {
            Error = new Error();
            Error.code = 0;
            Error.message = message;
        }

        public JsonRpcException(string format, params object[] args)
            : base(string.Format(format, args))
        {
            Error = new Error();
            Error.code = 0;
            Error.message = string.Format(format, args);
        }

        public JsonRpcException(int code, string message, string explanation)
            : base(message)
        {
            Error = new Error();
            Error.code = code;
            Error.message = message;
        }

        protected JsonRpcException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public JsonRpcException(Error err)
            : this(err.code,err.message,null)
        {
            
        }

        public Error Error { get; set; }
    }
}
