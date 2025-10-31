using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class CountryCreator : IModelCreator<Country>
    {
        public Country CreateModel(IDataReader reader)
        {
            return new Country
            {
                 CountryId = Convert.ToString(reader["CountryId"]),
                 CountryName = Convert.ToString(reader["CountryName"])
            };
        }
    }
}
