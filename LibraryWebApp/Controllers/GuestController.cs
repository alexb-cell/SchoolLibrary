using Microsoft.AspNetCore.Mvc;
using LibraryWSClient;
using ModelLibrary;
using LibraryModels;
using System.Net;

namespace LibraryWebApp.Controllers
{
    public class GuestController : Controller
    {
        [HttpGet]
        public IActionResult HomePage()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewBookCatalog(string authorId = null,
                                            string ganreId = null, int page = 1)
        {
            ApiClient<CatalogViewModel> client = new ApiClient<CatalogViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetBookCatalog";
            if (page != 0)
                client.AddParameter("page", page.ToString());
            if (authorId != null)
                client.AddParameter("authorId", authorId);
            if (ganreId != null)
                client.AddParameter("ganreId", ganreId);
            CatalogViewModel catalogViewModel = await client.GetAsync();
            return View(catalogViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookDetals(string bookId)
        {
            ApiClient<BookViewModel> client = new ApiClient<BookViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetBook";
            client.AddParameter("bookId", bookId);
            BookViewModel bookViewModel = await client.GetAsync();
            return View(bookViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewRegistrationForm()
        {
            ApiClient<List<City>> client = new ApiClient<List<City>>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetCities";
            List<City> cities = await client.GetAsync();
            return View(cities);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Reader reader, IFormFile file)
        {
            reader.ReaderImage = file.FileName;
            ApiClient<Reader> client = new ApiClient<Reader>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetCities";
            bool ok = await client.PostAsync(reader, file.OpenReadStream());
            return View();

        }
    }
}
