using System.Data;
using System.Data.OleDb;

namespace SchoolLibraryWS
{
   
    public class DbHelperOledb : IDbHelper
    {
        OleDbConnection oleDbConnection;
        OleDbCommand dbCommand;
        OleDbTransaction dbTransaction;

        public DbHelperOledb()
        {
            this.oleDbConnection = new OleDbConnection();
            this.oleDbConnection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\AlexLesson\2025-2026\SchoolLibarary\SchoolLibraryWS\App_Data\Library.accdb;Persist Security Info=True";

           // this.oleDbConnection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetCurrentDirectory()}\App_Data\Library.accdb;Persist Security Info=True";
            this.dbCommand = new OleDbCommand();
            this.dbCommand.Connection = this.oleDbConnection;
        }
        public void CloseConnection()
        {
            this.oleDbConnection.Close();
        }

        public void Commit()
        {
            this.dbTransaction.Commit();
        }

        public int Delete(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteNonQuery();
        }

        public int Insert(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteNonQuery();
        }

        public void OpenConnection()
        {
            this.oleDbConnection.Open();
        }

        public void OpenTransaction()
        {
            this.dbTransaction = this.oleDbConnection.BeginTransaction();
        }

        public void RollBack()
        {
            this.dbTransaction.Rollback();
        }

        public IDataReader Select(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteReader();
        }

        public int Update(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteNonQuery();
        }
    }
}
