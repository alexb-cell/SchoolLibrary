using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class CityRepository : Repository, IRepository<City>
    {
        public CityRepository(DbHelperOledb dbHelperOledb,
                               ModelCreators modelCreators) :
                               base(dbHelperOledb, modelCreators)
        {

        }
        public bool Create(City item)
        {
            string sql = $@"INSERT INTO Cities
                            (CityId) VALUES (@CityName)";
            this.helperOledb.AddParameter("@CityName", item.CityName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Cities where CitiId=@CityId";
            this.helperOledb.AddParameter("@CityId", id);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<City> GetAll()
        {
            List<City> cities = new List<City>();
            string sql = "SELECT * FROM Cities";
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    cities.Add(this.modelCreators.CityCreator.CreateModel(reader));
                }
            }
            return cities;
        }

        public City GetById(string id)
        {
            string sql = "SELECT * FROM Cities where CityId=@Cityid";
            this.helperOledb.AddParameter("@CityName", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.CityCreator.CreateModel(reader);
            }
        }

        public bool Update(City item)
        {
            string sql = $@"Update Cities set CityName=@CityName)";
            this.helperOledb.AddParameter("@CityName", item.CityName);
            return this.helperOledb.Insert(sql) > 0;
        }
    }
}
