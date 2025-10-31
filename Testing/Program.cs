using LibraryModels;
using SchoolLibraryWS;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
namespace Testing
{
    internal class Program
    {

        static void CheckCreator()
        {
            string sql = "Select * from Authors where AuthorId=47";
            DbHelperOledb dbHelperOledb = new DbHelperOledb();
            dbHelperOledb.OpenConnection();
            IDataReader dataReader = dbHelperOledb.Select(sql);
            dataReader.Read();
            ModelCreators modelCreators = new ModelCreators();
            Author author = modelCreators.AuthorCreator.CreateModel(dataReader);
            dbHelperOledb.CloseConnection();
            Console.WriteLine($"{author.AuthorFirstName} {author.AuthorLastName}");
        }
       

        static void Main(string[] args)
        {
            CheckCreator();
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

                Dictionary<string,string> all = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
                foreach(KeyValuePair<string,string> keyValuePair in all)
                { Console.WriteLine(keyValuePair.Value); }

            }
        }
    }
}
