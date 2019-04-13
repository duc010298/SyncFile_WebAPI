using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sync_File_Client_Windows
{
    class Config
    {
        public static readonly string AuthenticationInfoFilePath = "AuthenticatonToken.dat";

        public static HttpClient Client { get; set; }

        static Config()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8080/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
