using LibraryModels;
using ModelLibrary;
using System.Collections.Generic;
using System.Data;

namespace SchoolLibraryWS
{
    public class AuthoRepository : Repository, IRepository<Author>
    {
        public AuthoRepository(DbHelperOledb helperOledb, ModelCreators modelCreators)
            :base(helperOledb, modelCreators)
        {

        }
        public bool Create(Author model)
        {
            //string sql = @$"Insert into Authors
            //                (
            //                  AuthorFirstName, AuthorLastName,
            //                  AuthorYear,CountryId,AuthorPicture
            //                )
            //                values
            //                (
            //                     '{model.AuthorFirstName}','{model.AuthorLastName}',
            //                     {model.AuthorYear}, {model.CountryId},'{model.AuthorPicture}'
            //                )";
            string sql = @$"Insert into Authors
                            (
                              AuthorFirstName, AuthorLastName,
                              AuthorYear,CountryId,AuthorPicture
                            )
                            values
                            (
                                 @AuthorFirstName,@AuthorLastName,
                                 @AuthorYear, @CountryId,@AuthorPicture
                            )";
            this.helperOledb.AddParameter("@AuthorFirstName", model.AuthorFirstName);
            this.helperOledb.AddParameter("@AuthorLastName", model.AuthorLastName);
            this.helperOledb.AddParameter("@AuthorYear", model.AuthorYear.ToString());
            this.helperOledb.AddParameter("@CountryId", model.CountryId.ToString());
            this.helperOledb.AddParameter("@AuthorPicture", model.AuthorPicture);
            return this.helperOledb.Insert(sql)>0;
        }



        public bool Create(string bookId, string authorId)
        {
            //string sql = @$"Insert into Authors
            //                (
            //                  AuthorFirstName, AuthorLastName,
            //                  AuthorYear,CountryId,AuthorPicture
            //                )
            //                values
            //                (
            //                     '{model.AuthorFirstName}','{model.AuthorLastName}',
            //                     {model.AuthorYear}, {model.CountryId},'{model.AuthorPicture}'
            //                )";
            string sql = @$"Insert into BooksAuthors
                            (
                              BookId, AuthorId
                            )
                            values
                            (
                                 @BookId,@AuthorId
                            )";
            this.helperOledb.AddParameter("@BookId", bookId);
            this.helperOledb.AddParameter("@AuthorId", authorId);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Authors where authorId=@authorId";
            this.helperOledb.AddParameter("@authorId", id);
            return this.helperOledb.Delete(sql) > 0;
        }

        public List<Author> GetAll()
        {
            string sql = "Select * from Authors";
            
            List<Author> authors = new List<Author>();
            using(IDataReader reader = this.helperOledb.Select(sql))
            {
               while (reader.Read())
               {
                authors.Add(this.modelCreators.AuthorCreator.CreateModel(reader));
               }
            }
           
            return authors;
        }

        public Author GetById(string id)
        {
            string sql = "Select * from Authors where AuthorId=@AuthorId";
            this.helperOledb.AddParameter("@authorId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.AuthorCreator.CreateModel(reader);
            }
        }

        public bool Update(Author model)
        {
            string sql = @"Update Authors set
                                             AuthorFirstName=@AuthorFirstName,
                                             AuthorLastName=@AuthorLastName.
                                             AuthorYear=@AuthorYear,
                                             CountryId=@CountryId,
                                             AuthorPicture=@AuthorPicture";
            this.helperOledb.AddParameter("@AuthorFirstName", model.AuthorFirstName);
            this.helperOledb.AddParameter("@AuthorLastName", model.AuthorLastName);
            this.helperOledb.AddParameter("@AuthorYear", model.AuthorYear.ToString());
            this.helperOledb.AddParameter("@CountryId", model.CountryId.ToString());
            this.helperOledb.AddParameter("@AuthorPicture", model.AuthorPicture);
            return this.helperOledb.Update(sql) > 0;

        }
    }
}
