using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class AuthorCreator : IModelCreator<Author>
    {
        public Author CreateModel(IDataReader dataReader)
        {
            //Author author = new Author();
            //author.AuthorFirstName = Convert.ToString(dataReader["AuthorFirstName"]);
            //author.AuthorId = Convert.ToString(dataReader["AuthorId"]);  
            //author.AuthorLastName = Convert.ToString(dataReader["AuthorLastName"]);
            //author.AuthorPicture = Convert.ToString(dataReader["AuthorPicture"]);
            //author.AuthorYear = Convert.ToUInt16(dataReader["AuthorYear"]);
            //author.CountryId = Convert.ToUInt16(dataReader["CountryId"]);
            //return author;

            Author author = new Author
            {
                AuthorFirstName = Convert.ToString(dataReader["AuthorFirstName"]),
                AuthorId = Convert.ToString(dataReader["AuthorId"]),
                AuthorLastName = Convert.ToString(dataReader["AuthorLastName"]),
                AuthorPicture = Convert.ToString(dataReader["AuthorPicture"]),
                AuthorYear = Convert.ToInt16(dataReader["AuthorYear"]),
                CountryId = Convert.ToInt16(dataReader["CountryId"])
            };
            return author;
        }
    }
}
