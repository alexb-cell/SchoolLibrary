using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SchoolLibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        RepositoryUOW repositoryUOW;
        public ReaderController()
        {
            this.repositoryUOW = new RepositoryUOW();
        }
        [HttpGet]
        public string Login(string nickName, string password)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                return this.repositoryUOW.ReaderRepository.Login(nickName, password);
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
        public Reader GetReaderById(string readerId)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                return this.repositoryUOW.ReaderRepository.GetById(readerId);
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
        public List<ReaderBorrow> GetReaderBorrows(string readerId)
        {
            List<ReaderBorrow> readerBorrows = new List<ReaderBorrow>();
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                List<Borrow> borrows = this.repositoryUOW.BorrowRepository.GetReaderBorrows(readerId);
                foreach (var borrow in borrows)
                {
                    Book book = this.repositoryUOW.BookRepository.GetById(borrow.BookId);
                    ReaderBorrow readerBorrow = new ReaderBorrow()
                    {
                        Borrow = borrow,
                        Book = book
                    };
                    readerBorrows.Add(readerBorrow);
                }
                return readerBorrows;
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
        public bool AddBorrow(string readerId, string bookId)
        {
            try
            {
                this.repositoryUOW.DbHelperOledb.OpenConnection();
                Borrow borrow = new Borrow()
                {
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    BorrowStatus = "1"
                };
                return this.repositoryUOW.BorrowRepository.Create(borrow);
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
    }
}
