using LibraryModels;
using LibraryWSClient;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryWebApp.Controllers
{
    public class ReaderController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ViewLoginForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nickName, string password)
        {
            ApiClient<string> client = new ApiClient<string>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/Login";
            client.AddParameter("nickName", nickName);
            client.AddParameter("password", password);
            string id = await client.GetAsync();
            if (id != null)
                HttpContext.Session.SetString("readerId", id);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>  ViewReadersBorrows(string borrowStatus)
        {
            ApiClient<List<ReaderBorrow>> client = new ApiClient<List<ReaderBorrow>>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/GetReaderBorrows";
            string readerId = HttpContext.Session.GetString("readerId");
            client.AddParameter("readerId", readerId);
            client.AddParameter("borrowStatus", borrowStatus);
            List<ReaderBorrow> readerBorrow = await client.GetAsync();
            return View(readerBorrow);
            
        }

        
        [HttpGet]
        public async Task<IActionResult> MakeOrder(string bookId)
        {
            ApiClient<bool> client = new ApiClient<bool>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5273;
            client.Path = "api/Guest/AddBorrow";
            string readerId = HttpContext.Session.GetString("readerId");
            client.AddParameter("readerId", readerId);
            client.AddParameter("bookId", bookId);
            bool ok= await client.GetAsync();
            return View();
        }


        

    }
}
