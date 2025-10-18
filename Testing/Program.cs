using LibraryModels;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestCurrencyApi2();

            Console.ReadLine();
        }

        static void TestBook()
        {
            Book book = new Book();
            book.BookId = "1";
            book.BookImage = "hdhfjdh.jpg";
            book.BookName = "Asas Afgofoigo";
            book.BookDescription = "dkjkdfkdkfj";
            if (book.HasErrors == true)
            {
                foreach (KeyValuePair<string, List<string>> keyValuePair in book.AllErrors())
                {
                    Console.WriteLine(keyValuePair.Key);
                    foreach (string str in keyValuePair.Value)
                    {
                        Console.WriteLine($"    {str}");
                    }
                    Console.WriteLine("============================");
                }
            }
            else
                Console.WriteLine("There wos not Errors");
        }

        static async Task CurrensyList()
        {
            Console.Write(">>> Insert Currency from ");
            string from = Console.ReadLine();
            Console.Write(">>> Insert Currency to ");
            string to = Console.ReadLine();
            Console.Write(">>> Insert Amount ");
            string amount = Console.ReadLine();
            Uri uri = new Uri($"https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from={from}&to={to}&amount={amount}");
            Console.WriteLine(uri.AbsoluteUri);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from={from}&to={to}&amount={amount}"),
                Headers =
    {
        { "x-rapidapi-key", "00d5504b3emsh72ea7a8b3c0063bp18b92ejsn5b95ebcee531" },
        { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
    },
            };
            using (
                var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Currency carr = JsonSerializer.Deserialize<Currency>(body);

                Console.WriteLine($"{carr.query.amount} {carr.query.from} = {carr.result}{carr.query.to}");
            }
        }

        static async Task GetSymbols()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/symbols"),
                Headers =
    {
        { "x-rapidapi-key", "00d5504b3emsh72ea7a8b3c0063bp18b92ejsn5b95ebcee531" },
        { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //  Console.WriteLine(body);

                Dictionary<string, string> all = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
                foreach (KeyValuePair<string, string> keyValuePair in all)
                { Console.WriteLine(keyValuePair.Value); }

            }
        }

        static async Task TestCurrencyApi()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://exchange-rates7.p.rapidapi.com/codes"),
                Headers =
    {
        { "x-rapidapi-key", "f941083369msh224539dcf0b2026p1db183jsna6addf01cf75" },
        { "x-rapidapi-host", "exchange-rates7.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                CurrencyTest currencyTest = JsonSerializer.Deserialize<CurrencyTest>(body); 
                foreach(CurrencyCode code in currencyTest.supported_codes)
                {
                    Console.WriteLine($"{code.code}  ({code.name})");
                }
            }
        }

         static async Task TestCurrencyApi2()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/symbols"),
                Headers =
    {
        { "x-rapidapi-key", "f941083369msh224539dcf0b2026p1db183jsna6addf01cf75" },
        { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                try
                {
                    CurrencySymbols symbols = JsonSerializer.Deserialize<CurrencySymbols>(body);
                    foreach (KeyValuePair<string,string> keyValuePair in symbols.symbols)
                    {
                        Console.WriteLine(keyValuePair.Key+ " " + keyValuePair.Value);
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                
                //Console.WriteLine(body);
            }
        }
    }
}
