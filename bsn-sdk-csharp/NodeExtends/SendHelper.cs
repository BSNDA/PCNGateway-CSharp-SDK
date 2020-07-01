using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    /// <summary>
    /// send ServiceHelper class
    /// </summary>
    public static class SendHelper
    {
        /// <summary>
        /// Specify the Post address to get all the strings in a GET request
        /// </summary>
        /// <param name="url">request backstage address</param>
        /// <param name="content">Post submit data content (utf-8 coded)</param>
        /// <param name="certPath">cert address</param>
        /// <returns></returns>
        public static T SendPost<T>(string url, string content, string certPath)
        {
            System.Console.WriteLine(string.Format("request address：{0}，request message：{1}", url, content));
            try
            {
                //build the Http client handler
                var handler = new HttpClientHandler();
                if (!string.IsNullOrEmpty(certPath))
                {
                    //set the option of client cert to manual
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    //add a public key cert in X509Certificate2 format
                    handler.ClientCertificates.Add(new X509Certificate2(certPath, "", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
                    //Setup SSL protocol
                    handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                    //verify and callback the server cert
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                }
                //build the HttpClient
                using (HttpClient client = new HttpClient(handler))
                {
                    //Build the MemoryStream object
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Convert the request content into a byte array
                        byte[] bytes = Encoding.UTF8.GetBytes(content);
                        ms.Write(bytes, 0, bytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);//set cursor position, otherwise sending invalid
                        HttpContent hc = new StreamContent(ms);
                        //asynchronous sending
                        var response = client.PostAsync(url, hc).Result;
                        //Get the response result of the server
                        string result = response.Content.ReadAsStringAsync().Result;
                        //check if the response status is 200
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            System.Console.WriteLine(string.Format("response message：{0}", result));
                            return JsonConvert.DeserializeObject<T>(result);
                        }
                        else
                        {
                            string httpStatus = System.Enum.GetName(typeof(HttpStatusCode), response.StatusCode);
                            System.Console.WriteLine("Non 200HTTP status，httpStatus=" + httpStatus + "，data=[" + result + "]");
                            return default(T);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("post failure，error：" + ex.Message);
                return default(T);
            }
        }
    }
}