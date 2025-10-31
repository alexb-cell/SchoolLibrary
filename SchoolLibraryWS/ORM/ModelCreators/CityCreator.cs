using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class CityCreator : IModelCreator<City>
    {
        public City CreateModel(IDataReader reader)
        {
            return new City
            {
                CityId = Convert.ToString(reader["CityId"]),
                CityName = Convert.ToString(reader["CityName"]),
            };
        }
    }
}
