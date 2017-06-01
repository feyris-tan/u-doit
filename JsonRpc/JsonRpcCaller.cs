using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.JsonRpc
{
    public class JsonRpcCaller
    {
        public JsonRpcCaller(string baseUrl)
        {
            this.BaseURL = baseUrl;
            this.Json = new JsonSerializer();
            this.encoding = new UTF8Encoding();
            this.settings = new JsonSerializerSettings();

            

            ServicePointManager.Expect100Continue = false;   //Weil wir kein IIS verwenden wollen!
        }

        public string BaseURL{ get; private set; }
        private JsonSerializer Json;
        private UTF8Encoding encoding;
        private JsonSerializerSettings settings;
        public string XRpcAuthSession { private get; set; }
        public string XRpcAuthUsername { private get; set; }
        public string XRpcAuthPassword { private get; set; }

        public object Call(string methodName, object args)
        {
            if (args == null) args = new {};
            Request req = new Request();
            req.method = methodName;
            req.@params = args;


            string json = JsonConvert.SerializeObject(req, Formatting.Indented, settings);

            var http = (HttpWebRequest) WebRequest.Create(BaseURL);
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            if (!string.IsNullOrEmpty(XRpcAuthSession)) http.Headers.Add("X-RPC-Auth-Session", XRpcAuthSession);
            if (!string.IsNullOrEmpty(XRpcAuthUsername)) http.Headers.Add("X-RPC-Auth-Username", XRpcAuthUsername);
            if (!string.IsNullOrEmpty(XRpcAuthPassword)) http.Headers.Add("X-RPC-Auth-Password", XRpcAuthPassword);

            byte[] bytes = encoding.GetBytes(json);
            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            string jsonResponse = sr.ReadToEnd();


            Response r = JsonConvert.DeserializeObject<Response>(jsonResponse);

            if (Config.LogAllJson)
            {
                Console.WriteLine(r.result.ToString());
            }

            if (r.error != null)
            {
                throw new JsonRpcException(r.error);
            }
            return r.result;
        }

        public T Call<T>(string methodName, object args)
        {
            object temp = Call(methodName, args);
            try
            {
                T result = JsonConvert.DeserializeObject<T>(temp.ToString());
                return result;
            }
            catch (JsonSerializationException je)
            {
                Console.WriteLine("-> could not parse response of {0}, returning null!", methodName);
                return default(T);
            }
            
        }

    }
}
