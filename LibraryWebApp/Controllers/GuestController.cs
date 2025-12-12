using Microsoft.AspNetCore.Mvc;
using LibraryWSClient;
using ModelLibrary;
using LibraryModels;

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
                                            string ganreId = null, int page = 0)
        {
            ApiClient<CatalogViewModel> client = new ApiClient<CatalogViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetBookCatalog";
            if (authorId != null)
                client.AddParameter("authorId", authorId);
            if(ganreId !=null)
                client.AddParameter("ganreId", ganreId);
            if(page!=0)
                client.AddParameter("page", page.ToString());
            CatalogViewModel catalogViewModel = await client.GetAsync();
            return View(catalogViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookDetals(string bookId)
        {
            ApiClient<Book> client = new ApiClient<Book>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetBook";
            client.AddParameter("bookId", bookId);
            Book book = await client.GetAsync();
            return View(book);
        }


    }
}
