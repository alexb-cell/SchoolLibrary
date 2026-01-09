using Microsoft.AspNetCore.Mvc;
using LibraryWSClient;
using ModelLibrary;
using LibraryModels;
using System.Net;
using System.IO;

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
            RegistrationViewModel registrationViewModel =new RegistrationViewModel();
            registrationViewModel.Reader = null;
            ApiClient<List<City>> client = new ApiClient<List<City>>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetCities";
            registrationViewModel.Cities = await client.GetAsync();
            return View(registrationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Reader reader, IFormFile file)
        {
            List<City> cities = await GetAllCitiesAsync();
            if (ModelState.IsValid == false)
            {
                RegistrationViewModel registrationViewModel = new RegistrationViewModel();
                registrationViewModel.Reader = reader;
                registrationViewModel.Cities = cities;
                return View("ViewRegistrationForm", registrationViewModel);
            }
            bool ok =await SendReader(reader, file);
            if (ok == true)
            {
                HttpContext.Session.SetString("readerId", reader.ReaderId);
                return RedirectToAction("GetReaderBorrows", "Reader");
            }
            RegistrationViewModel registrationViewModel2 = new RegistrationViewModel();
            registrationViewModel2.Reader = reader;
          
            registrationViewModel2.Cities = cities;
            ViewBag.Error = true;
            return View("ViewRegistrationForm", registrationViewModel2);

           

        }

        private async Task<List<City>> GetAllCitiesAsync()
        {
            ApiClient<List<City>> client = new ApiClient<List<City>>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetCities";
            return await client.GetAsync();

        }

        private async Task<bool> SendReader(Reader reader, IFormFile file)
        {
            ApiClient<Reader> clientReader = new ApiClient<Reader>();
            reader.ReaderImage = Path.GetExtension(file.FileName);
            clientReader.Scheme = "http";
            clientReader.Host = "localhost";
            clientReader.Port = 5273;
            clientReader.Path = "api/Guest/RegisterReader";
            return  await clientReader.PostAsync(reader, file.OpenReadStream());
        }
    }
}
