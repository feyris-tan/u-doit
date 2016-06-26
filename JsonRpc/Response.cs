using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit.JsonRpc
{
    public class Response
    {
        public object result;
        public Error error;
        public int id;
    }

    public class Error
    {
        public int code;
        public string message;
        public object data;
    }
}
