using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class GanreRepository : Repository, IRepository<Ganre>
    {
        public GanreRepository(DbHelperOledb dbHelperOledb,
                             ModelCreators modelCreators) :
                             base(dbHelperOledb, modelCreators)
        {

        }
        public bool Create(Ganre item)
        {
            string sql = $@"INSERT INTO Ganres
                            (GanreId) VALUES (@GanreName)";
            this.helperOledb.AddParameter("@GanreName", item.GanreName);
            return this.helperOledb.Insert(sql) > 0;
        }
        public bool Create(string bookId, string ganreId)
        {
            string sql = $@"INSERT INTO BooksGenres
                            (BookId,TypeBookId) VALUES (@BookId, @TypeBookId)";
            this.helperOledb.AddParameter("@BookId", bookId);
            this.helperOledb.AddParameter("@TypeBookId", ganreId);
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
            string sql = "SELECT * FROM Genres";
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
