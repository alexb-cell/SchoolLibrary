using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class BorrowRepository : Repository, IRepository<Borrow>
    {
        public BorrowRepository(DbHelperOledb dbHelperOledb,
                              ModelCreators modelCreators) :
                              base(dbHelperOledb, modelCreators)
        {

        }
        public bool Create(Borrow item)
        {
            string sql = $@"INSERT INTO Borrows
                            (
                              ReaderId, 
                              BorrowDate, 
                              BorrowStatus,BookId
                            )
                           VALUES
                           (
                               @ReaderId,  @BorrowDate,
                               @BorrowStatus, @BookId
                           )";
            this.helperOledb.AddParameter("@ReaderId", item.ReaderId);
            this.helperOledb.AddParameter("@BorrowDate", item.BorrowDate);
            this.helperOledb.AddParameter("@BorrowStatus", item.BorrowStatus);
            this.helperOledb.AddParameter("@BookId", item.BookId);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Borrows where BorrowId=@BorrowId";
            this.helperOledb.AddParameter("@BorrowId", id);
            return this.helperOledb.Insert(sql) > 0;
        }

        public List<Borrow> GetAll()
        {
            List<Borrow> borrows = new List<Borrow>();
            string sql = "SELECT * FROM Borrows";
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    borrows.Add(this.modelCreators.BorrowCreator.CreateModel(reader));
                }
            }
            return borrows;

        }

        public Borrow GetById(string id)
        {
            string sql = $"SELECT * FROM Borrows where Borrowid=@BorrowId";
            this.helperOledb.AddParameter("@BorrowId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreators.BorrowCreator.CreateModel(reader);
            }
        }

        public bool Update(Borrow item)
        {
            string sql = $@"Update set Borrows
                              ReaderId=@ReaderId, 
                              BorrowDate=@BorrowDate, 
                              BorrowStatus=@BorrowStatus,
                              BookId=@BookId
                              Where BorrowId=@BorrowId";
            this.helperOledb.AddParameter("@ReaderId", item.ReaderId);
            this.helperOledb.AddParameter("@BorrowDate", item.BorrowDate);
            this.helperOledb.AddParameter("@BorrowStatus", item.BorrowStatus);
            this.helperOledb.AddParameter("@BookId", item.BookId);
            this.helperOledb.AddParameter("@BorrowId", item.BorrowId);
            return this.helperOledb.Insert(sql) > 0;
        }
    }
}
