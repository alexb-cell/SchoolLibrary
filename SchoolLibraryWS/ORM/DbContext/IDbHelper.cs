using System.Data;

namespace SchoolLibraryWS
{
    public interface IDbHelper
    {
        void OpenConnection();

        void CloseConnection();

        IDataReader Select(string sql);

        // CRUD
        int Update(string sql);
        int Insert(string sql);
        int Delete(string sql);

        void OpenTransaction();

        void Commit();

        void RollBack();

    }
}
