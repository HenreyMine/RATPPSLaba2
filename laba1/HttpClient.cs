using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace laba1
{
    public class HttpClient : IHttpClient
    {
        public string GetInputData(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/GetInputData");
            var response = (HttpWebResponse)request.GetResponse();
            var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return stream.ReadToEnd();
        }

        public bool Ping(string url)
        {
            HttpWebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url + "/Ping");
                response = (HttpWebResponse)request.GetResponse();
            }
            catch
            {
                return false;
            }
            return response?.StatusCode == HttpStatusCode.OK;
        }

        public void WriteAnswer(string url, string json)
        {
            var body = Encoding.UTF8.GetBytes(json);
            var request = (HttpWebRequest)WebRequest.Create(url + "/WriteAnswer");
            request.Method = "POST";
            var requestStream = request.GetRequestStream();
            requestStream.Write(body, 0, body.Length);
            var requestResponse = (HttpWebResponse)request.GetResponse();
        }
    }
}
