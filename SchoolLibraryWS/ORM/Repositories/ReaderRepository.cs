using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class ReaderRepository : Repository, IRepository<Reader>
    {
       
        public bool Create(Reader item)
        {
            string sql = $@"INSERT INTO Readers
                            (
                              ReaderId, 
                              ReaderFirstName, ReaderLastName,
                              ReaderAdress,ReaderTelephone,
                              ReaderImage, CityId,
                              ReaderNickName, ReaderPassword
                            )
                           VALUES
                           (
                               @ReaderId,  @ReaderFirstName,
                               @ReaderLastName, @ReaderAdress,@ReaderTelephone,
                               @ReaderImage, @CityId, 
                               @ReaderNickName, @ReaderPassword
                           )";
            this.helperOledb.AddParameter("@ReaderId", item.ReaderId);
            this.helperOledb.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.helperOledb.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.helperOledb.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.helperOledb.AddParameter("@CityId", item.CityId);
            this.helperOledb.AddParameter("@ReaderTelephone", item.ReaderTelephone);
            this.helperOledb.AddParameter("@ReaderImage", item.ReaderImage);
            this.helperOledb.AddParameter("@ReaderNickName", item.ReaderNickName);
            this.helperOledb.AddParameter("@ReaderPassword", item.ReaderPassword);

            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Readers where ReaderId=@ReaderId";
            this.helperOledb.AddParameter("@ReaderId", id);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<Reader> GetAll()
        {
            List<Reader> readers = new List<Reader>();
            string sql = "SELECT * FROM Readers";
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    readers.Add(this.modelCreators.ReaderCreator.CreateModel(reader));
                }
            }
            return readers;
        }

        public Reader GetById(string id)
        {
            string sql = $"SELECT * FROM Readers where Readerid=@ReaderId";
            this.helperOledb.AddParameter("@ReaderId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.ReaderCreator.CreateModel(reader);
            }
        }

        public bool Update(Reader item)
        {
            string sql = $@"INSERT set Readers
                              ReaderId=@ReaderId, 
                              ReaderFirstName=@ReaderFirstName,
                              ReaderLastName=@ReaderLastName,
                              ReaderAdress=@ReaderAdress,
                              ReaderTelephone=@ReaderTelephone,
                              ReaderImage=@ReaderImage,
                              CityId=@CityId,
                              ReaderNickName=@ReaderNickName,
                              ReaderPassword=@ReaderPassword";
            this.helperOledb.AddParameter("@ReaderId", item.ReaderId);
            this.helperOledb.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.helperOledb.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.helperOledb.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.helperOledb.AddParameter("@CityId", item.CityId);
            this.helperOledb.AddParameter("@ReaderTelephone", item.ReaderTelephone);
            this.helperOledb.AddParameter("@ReaderImage", item.ReaderImage);
            this.helperOledb.AddParameter("@ReaderNickName", item.ReaderNickName);
            this.helperOledb.AddParameter("@ReaderPassword", item.ReaderPassword);

            return this.helperOledb.Insert(sql) > 0;
        }

      
    }
}
