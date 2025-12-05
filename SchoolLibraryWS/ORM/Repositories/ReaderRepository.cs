using LibraryModels;
using System.Data;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Text.Unicode;

namespace SchoolLibraryWS
{
    public class ReaderRepository : Repository, IRepository<Reader>
    {
        public ReaderRepository(DbHelperOledb dbHelperOledb,
                              ModelCreators modelCreators) :
                              base(dbHelperOledb, modelCreators)
        {

        }
        public bool Create(Reader item)
        {
            string sql = $@"INSERT INTO Readers
                            (
                              ReaderId, 
                              ReaderFirstName, ReaderLastName,
                              ReaderAdress,ReaderTelephone,
                              ReaderImage, CityId,
                              ReaderNickName, ReaderPassword, ReaderSalt
                            )
                           VALUES
                           (
                               @ReaderId,  @ReaderFirstName,
                               @ReaderLastName, @ReaderAdress,@ReaderTelephone,
                               @ReaderImage, @CityId, 
                               @ReaderNickName, @ReaderPassword, @ReaderSalt
                           )";
            this.helperOledb.AddParameter("@ReaderId", item.ReaderId);
            this.helperOledb.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.helperOledb.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.helperOledb.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.helperOledb.AddParameter("@CityId", item.CityId);
            this.helperOledb.AddParameter("@ReaderTelephone", item.ReaderTelephone);
            this.helperOledb.AddParameter("@ReaderImage", item.ReaderImage);
            this.helperOledb.AddParameter("@ReaderNickName", item.ReaderNickName);
            string salt = GetSalt(GetRandom());
            this.helperOledb.AddParameter("@ReaderPassword", GetHash(item.ReaderPassword,salt));
            this.helperOledb.AddParameter("@ReaderSalt",salt);
            return this.helperOledb.Insert(sql) > 0;
        }

        private string GetHash(string password, string salt)
        {
            string combine = password + salt;
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(combine);
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private string GetSalt(int lenght)
        {
            byte[] bytes = new byte[lenght];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);

        }

        private int GetRandom()
        {
            Random rnd = new Random();
            return rnd.Next(8,16);
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

        public string Login(string nickName, string password)
        {
            string sql = @"Select ReaderSalt, ReaderId, ReaderPassword
                           from Readers 
                           where ReaderNickName=@ReaderNickName";
            this.helperOledb.AddParameter("@ReaderNickName", nickName);
            using(IDataReader reader = this.helperOledb.Select(sql))
            {
                if (reader.Read() == true)
                {
                    string salt = reader["ReaderSalt"].ToString();
                    string hash = reader["ReaderPassword"].ToString();
                    string calculateHash = GetHash(password, salt);
                    if(hash == calculateHash)
                         return reader["ReaderId"].ToString();
                }
                    
                return null;
            }

        }
    }
}
