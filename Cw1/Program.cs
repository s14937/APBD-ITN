using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://www.pja.edu.pl");

            if (response.IsSuccessStatusCode) //kod 2xx
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z0-9]+@[a-z.]+");

                MatchCollection matches = regex.Matches(html);
                foreach(var i in matches)
                {
                    Console.WriteLine(i);
                }
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}
