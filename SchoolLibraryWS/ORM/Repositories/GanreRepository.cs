using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class GanreRepository : Repository, IRepository<Ganre>
    {
        
        public bool Create(Ganre item)
        {
            string sql = $@"INSERT INTO Ganres
                            (GanreId) VALUES (@GanreName)";
            this.helperOledb.AddParameter("@GanreName", item.GanreName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Ganres where GanreId=@GanreId";
            this.helperOledb.AddParameter("@GanreId", id);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<Ganre> GetAll()
        {
            List<Ganre> ganres = new List<Ganre>();
            string sql = "SELECT * FROM Ganres";
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    ganres.Add(this.modelCreators.GanreCreator.CreateModel(reader));
                }
            }
            return ganres;
        }

        public Ganre GetById(string id)
        {
            string sql = "SELECT * FROM Ganres where GanreId=@GanreId";
            this.helperOledb.AddParameter("@GanreId", id);

            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.GanreCreator.CreateModel(reader);
            }
        }

        public bool Update(Ganre item)
        {
            string sql = $@"Update Ganres set GanreName=@GanreName)";
            this.helperOledb.AddParameter("@GanreName", item.GanreName);
            return this.helperOledb.Insert(sql) > 0;
        }
    }
}
