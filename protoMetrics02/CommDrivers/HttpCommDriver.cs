using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace doberSoft.protoMetrics02.CommDrivers
{
    class HttpCommDriver : ICommDriver
    {
        string EndPoint = "http://192.168.101.72:8080/";
        public bool Send(string payload, string destination)
        {

            HttpWebRequest httpWebRequest;
            Console.WriteLine("  Create req ");
            // usare webclinet
            httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPoint + destination);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Console.WriteLine("    Stream write");
                streamWriter.Write(payload);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine("  response: " + httpResponse.StatusCode);
            httpResponse.Close();
            return false;
        }
    }
}
