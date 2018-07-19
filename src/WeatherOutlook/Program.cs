using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Serilog;

namespace WeatherOutlook
{
    // A simple demo on creating a client to access a public API and store the response in a file
    class Program
    {
        private const string Url = "https://api.openweathermap.org/data/2.5/weather";
        //Get access token from https://openweathermap.org and input key on the app.config file
        private static readonly string AccessToken = ConfigurationManager.AppSettings["AccessToken"];

        static void Main(string[] args)
        {
            Console.Write("Input location(E.g London,uk):");
            var location = Console.ReadLine();
            string urlParameters = $"?q={location}&appid={AccessToken}";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync();
                Console.Write(result.Result);
                WriteToFile(response.RequestMessage.ToString(), result);
                Log.Information(response.Content.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Log.Error(response.StatusCode.ToString());
            }
            client.Dispose();
            Console.WriteLine("Press Q to exit");
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                // Press q to exit.
            }
        }

        public static async void WriteToFile(string request, Task<string> response)
        {
            using (StreamWriter writer = new StreamWriter("Request.txt"))
            {
                writer.Write(request);
                writer.Write(await response);
            }
        }
    }
}
