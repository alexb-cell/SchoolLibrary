using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryModels;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace SchoolLibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        RepositoryUOW repositoryUOW;

        public GuestController()
        {
            this.repositoryUOW = new RepositoryUOW();
        }

        [HttpGet]
        public CatalogViewModel GetBookCatalog(string authorId = null, string ganreId = null, int page = 1)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            catalogViewModel.GanreId = ganreId;
            catalogViewModel.AuthorId = authorId;
            catalogViewModel.Page = page;
            catalogViewModel.PagePerPage = 10;
            try
            {
                int booksperPage = 10;
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                catalogViewModel.Ganres = this.repositoryUOW.GanreRepository.GetAll();
                catalogViewModel.Authors = this.repositoryUOW.AuthoRepository.GetAll();
                if (authorId == null && ganreId == null && page >= 1)
                {
                    catalogViewModel.Books  = this.repositoryUOW.BookRepository.GetAll(); 
                }

                else if (authorId != null && ganreId == null && page == 1)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyAuthor(authorId);
                   

                }
                else if (authorId == null && ganreId != null && page >= 1)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyGanre(ganreId);

                }
               
                else if (authorId != null && ganreId == null && page != 1)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyAuthor(authorId);
                }
                else if (authorId == null && ganreId != null && page != 1)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyGanre(ganreId);
                }
                int books = catalogViewModel.Books.Count;
                if (books > booksperPage) 
                    catalogViewModel.Books=catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();
                catalogViewModel.PageCount = books/ catalogViewModel.PagePerPage;
                if (books % catalogViewModel.PagePerPage > 0)
                    catalogViewModel.PageCount++;
                return catalogViewModel;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                this.repositoryUOW.DbHelperOledb.CloseConnection();
            }

        }

        [HttpGet]
        public Book GetBook(string bookId)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                return this.repositoryUOW.BookRepository.GetById(bookId);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.repositoryUOW.DbHelperOledb.CloseConnection();
            }
        }

        [HttpPost]   
        public bool RegisterReader()
        {
            
            string json = HttpContext.Request.Form["model"];
            Reader reader = JsonSerializer.Deserialize<Reader>(json);
            IFormFile file = null;
            if(HttpContext.Request.Form.Files.Count >0)
            {
                file = HttpContext.Request.Form.Files[0];
            }
            try
            {
                repositoryUOW.DbHelperOledb.OpenTransaction();
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                bool ok= this.repositoryUOW.ReaderRepository.Create(reader);
                string path = $@"{Directory.GetCurrentDirectory()}\wwwroot\Images\Readers\{reader.ReaderId}.{reader.ReaderImage}";
                using (FileStream fileStream = new FileStream(path,FileMode.Open))
                {
                    fileStream.CopyTo(fileStream);
                }
                this.repositoryUOW.DbHelperOledb.Commit();
                return true;

            }
            catch (Exception ex)
            {
                this.repositoryUOW.DbHelperOledb.RollBack();
                return false;
            }
            finally
            {
                this.repositoryUOW.DbHelperOledb.CloseConnection();
            }
        }

        [HttpGet]
        public List<City> GetCities()
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                return this.repositoryUOW.CityRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.repositoryUOW.DbHelperOledb.CloseConnection();
            }

        }
    }
}
