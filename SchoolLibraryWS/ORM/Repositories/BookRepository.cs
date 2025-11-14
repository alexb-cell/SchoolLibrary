using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class BookRepository : Repository, IRepository<Book>
    {
       public BookRepository(DbHelperOledb dbHelperOledb,
                             ModelCreators modelCreators):
                             base(dbHelperOledb, modelCreators)
        {

        }

        public bool Create(Book model)
        {
            string sql = $@"INSERT INTO Books
                            (
                              BookName, 
                              BookDescription, 
                              BookImage,BookCopies
                            )
                           VALUES
                           (
                               @BookName,  @BookDescription,
                               @BookImage
                           )";
            this.helperOledb.AddParameter("@BookName", model.BookName);
            this.helperOledb.AddParameter("@BookDescription", model.BookDescription);
            this.helperOledb.AddParameter("@BookImage", model.BookImage);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Books where BookId=@BookId";
            this.helperOledb.AddParameter("@BookId", id);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<Book> GetAll()
        {
           
            string sql = "SELECT * FROM Books";
            return GetBookList(sql);
        }

        private List<Book> GetBookList(string sql)
        {
            List<Book> books = new List<Book>();
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.modelCreators.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }


        public List<Book> GetBooksbyGanre(string ganreId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksGenres.GenreId
                          FROM Books INNER JOIN BooksGenres ON Books.BookId = BooksGenres.BookId
                          WHERE BooksGenres.GenreId=@GanreId;";
            this.helperOledb.AddParameter("@GanreId", ganreId);
            return GetBookList(sql);
        }
        public List<Book> GetBooksbyAuthor(string AuthorId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksAuthors.AuthorId
                           FROM Books INNER JOIN BooksAuthors ON Books.BookId = BooksAuthors.BookId
                           WHERE (((BooksAuthors.AuthorId)=@AuthorId))";
            this.helperOledb.AddParameter("@AuthorId", AuthorId);
            return GetBookList(sql);
        }


        public Book GetById(string id)
        {
            string sql = $"SELECT * FROM Books where Bookid=@Bookid";
            this.helperOledb.AddParameter("@Bookid", id);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.BookCreator.CreateModel(reader);
            }
            return null;
        }

        public bool Update(Book item)
        {
            string sql = $@"Update set Books
                              BookName=@BookName, 
                              BookDescription=@BookName, 
                              BookImage=@BookName,
                              BookCopies=@BookName
                              where BookId=@BookId";
            this.helperOledb.AddParameter("@BookName", item.BookName);
            this.helperOledb.AddParameter("@BookDescription", item.BookDescription);
            this.helperOledb.AddParameter("@BookImage", item.BookImage);
            this.helperOledb.AddParameter("@BookId", item.BookId);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<Book> GetBooksByGanre(string ganreId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksGenres.TypeBookId
                           FROM Books INNER JOIN BooksGenres ON Books.BookId = BooksGenres.BookId
                           WHERE (((BooksGenres.TypeBookId)=@GanreId))"; ;
            this.helperOledb.AddParameter("@GanreId", ganreId);

            List<Book> books = new List<Book>();
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.modelCreators.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }
        public List<Book> GetBooksByAuthor(string authorId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksAuthors.AuthorId
                          FROM Books INNER JOIN BooksAuthors ON Books.BookId = BooksAuthors.BookId
                          WHERE (((BooksAuthors.AuthorId)=@AuthorId));";
            this.helperOledb.AddParameter("@AuthorId", authorId);

            List<Book> books = new List<Book>();
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.modelCreators.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }
    }
}
