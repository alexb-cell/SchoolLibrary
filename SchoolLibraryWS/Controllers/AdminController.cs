using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public bool AddNewBook(NewBookViewModel newBookViewModel)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                this.repositoryUOW.BeginTransaction();
                bool ok = this.repositoryUOW.BookRepository.Create(newBookViewModel.Book);
                string bookId = this.repositoryUOW.DbHelperOledb.GetLastInsertedId().ToString();
                foreach (string authorid in newBookViewModel.Authors)
                {
                    ok = ok && this.repositoryUOW.BookRepository.AddBookAuthor(bookId, authorid);
                }
                foreach (string ganreId in newBookViewModel.Genres)
                {
                    ok = ok && this.repositoryUOW.BookRepository.AdBookGanre(bookId, ganreId);
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

    }
}
