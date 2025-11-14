using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class CountryRepository : Repository, IRepository<Country>
    {
        public CountryRepository(DbHelperOledb dbHelperOledb,
                               ModelCreators modelCreators) :
                               base(dbHelperOledb, modelCreators)
        {

        }

        public bool Create(Country item)
        {
            string sql = $@"INSERT INTO Coutries
                            (CountryId) VALUES (@CountryName)";
            this.helperOledb.AddParameter("@CountryName", item.CountryName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Countries where CountryId=@CountryId";
            this.helperOledb.AddParameter("@CountryId", id);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<Country> GetAll()
        {
            List<Country> countries = new List<Country>();
            string sql = "SELECT * FROM Countries";
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    countries.Add(this.modelCreators.CountryCreator.CreateModel(reader));
                }
            }
            return countries;
        }

        public Country GetById(string id)
        {
            string sql = "SELECT * FROM Countries where CountryId=@Countryid";
            this.helperOledb.AddParameter("@CountryId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.CountryCreator.CreateModel(reader);
            }
        }

        public bool Update(Country item)
        {
            string sql = $@"Update Countries set CountryName=@CountryName)";
            this.helperOledb.AddParameter("@CountryName", item.CountryName);
            return this.helperOledb.Insert(sql) > 0;
        }
    }
}
