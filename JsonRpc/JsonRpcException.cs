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
            this.Code = 0;
            this.Message = null;
        }

        public JsonRpcException(string message) : base(message)
        {
            this.Code = 0;
            this.Message = message;
        }

        public JsonRpcException(string message, Exception inner) : base(message, inner)
        {
            this.Code = 0;
            this.Message = message;
        }

        public JsonRpcException(string format, params object[] args)
            : base(string.Format(format, args))
        {
            this.Code = 0;
            this.Message = string.Format(format, args);
        }

        public JsonRpcException(int code, string message, string explanation)
            : base(message)
        {
            this.Code = code;
            this.Message = message;
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

        public int Code { get; private set; }
        public new string Message { get; private set; }
    }
}
