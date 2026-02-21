using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace SchoolLibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        RepositoryUOW repositoryUOW;
        public AdminController()
        {
            this.repositoryUOW = new RepositoryUOW();
        }
        [HttpPost]
        public async Task<bool> AddNewBook()
        {
            string json = Request.Form["model"].ToString();
            NewBookViewModel newBookViewModel = 
                            JsonSerializer.Deserialize<NewBookViewModel>(json);
            IFormFile image = Request.Form.Files["file"];
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                this.repositoryUOW.BeginTransaction();
                bool ok = this.repositoryUOW.BookRepository.Create(newBookViewModel.Book);
                string id = this.repositoryUOW.GetLastInsertedId();

                foreach (Author author in newBookViewModel.Authors)
                {
                    this.repositoryUOW.AuthoRepository.Create(id, author.AuthorId);
                }
                foreach (Ganre genre in newBookViewModel.Genres)
                {
                    this.repositoryUOW.GanreRepository.Create(id, genre.GanreId);
                }
                string fileName = $"{id}{newBookViewModel.Book.BookImage}";
                this.repositoryUOW.BookRepository.UpdateImageName(id, fileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                                           "wwwroot", 
                                           "Images",
                                           "Books",
                                           fileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                this.repositoryUOW.Commit();
                return true;

            }
            catch
            {
                this.repositoryUOW.Rollback();
                return false;
            }
            finally
            {
                this.repositoryUOW.DbHelperOledb.CloseConnection();
            }
        }

        [HttpPost]
        public bool UpdateBook(UpdateBookViewModel  updateBookViewModel)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                this.repositoryUOW.BeginTransaction();
                bool ok = this.repositoryUOW.BookRepository.Update(updateBookViewModel.Book);
                foreach(string authorid in updateBookViewModel.AuthorsToDelete)
                {
                    ok = ok && this.repositoryUOW.BookRepository.DeleteBookAuthor(updateBookViewModel.Book.BookId, authorid);
                }
                foreach (string ganreId in updateBookViewModel.GanresToDelete)
                {
                    ok = ok && this.repositoryUOW.BookRepository.DeleteBookGanre(updateBookViewModel.Book.BookId, ganreId);
                }   
                foreach (string authorid in updateBookViewModel.AuthorsToAdd)
                {
                    ok = ok && this.repositoryUOW.BookRepository.AddBookAuthor(updateBookViewModel.Book.BookId, authorid);
                }
                foreach (string ganreId in updateBookViewModel.GanresToAdd)
                {
                    ok = ok && this.repositoryUOW.BookRepository.AdBookGanre(updateBookViewModel.Book.BookId, ganreId);
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
        public bool DeleteBook(string bookId)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                this.repositoryUOW.BeginTransaction();
                bool ok = this.repositoryUOW.BookRepository.GeleteBookAuthors(bookId);
                ok = ok && this.repositoryUOW.BookRepository.DeleteBookGanres(bookId);
                ok = ok && this.repositoryUOW.BookRepository.Delete(bookId);
                this.repositoryUOW.DbHelperOledb.Commit();
                return ok;
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
        public List<Book> GetBooks()
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                return this.repositoryUOW.BookRepository.GetAll();
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


        [HttpGet]
        public NewBookViewModel GetNewBookViewModel()
        {
            NewBookViewModel newBookViewModel = new NewBookViewModel();
            newBookViewModel.Book = null;
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                newBookViewModel.Genres = this.repositoryUOW.GanreRepository.GetAll();
                newBookViewModel.Authors = this.repositoryUOW.AuthoRepository.GetAll();
                return newBookViewModel;
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
