using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryModels;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.PortableExecutable;

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
        public CatalogViewModel GetBookCatalog(string authorId = null, string ganreId = null, int page = 0)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            catalogViewModel.GanreId = ganreId;
            catalogViewModel.AuthorId = authorId;
            catalogViewModel.Page = page;
            catalogViewModel.PagePerPage = 10;
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                catalogViewModel.Ganres = this.repositoryUOW.GanreRepository.GetAll();
                catalogViewModel.Authors = this.repositoryUOW.AuthoRepository.GetAll();
                if (authorId == null && ganreId == null && page == 0)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetAll();
                }
                else if (authorId != null && ganreId == null && page == 0)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyAuthor(authorId);
                }
                else if (authorId == null && ganreId != null && page == 0)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyGanre(ganreId);
                }
                else if (authorId == null && ganreId == null && page != 0)
                {
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksByPage(page);
                }
                else if (authorId != null && ganreId == null && page != 0)
                {
                    int booksperPage = 10;
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyAuthor(authorId);
                    catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();

                }
                else if (authorId == null && ganreId != null && page != 0)
                {
                    int booksperPage = 10;
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyGanre(ganreId);
                    catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();
                }
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
        public bool RegisterReader([FromBody] Reader reader)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                return this.repositoryUOW.ReaderRepository.Create(reader);
            }
            catch (Exception ex)
            {
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
