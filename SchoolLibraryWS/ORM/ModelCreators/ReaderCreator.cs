using LibraryModels;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibraryWS
{
    public class ReaderCreator : IModelCreator<Reader>
    {
        public Reader CreateModel(IDataReader reader)
        {
            return new Reader
            {
                CityId = Convert.ToString(reader["CityId"]),
                ReaderFirstName = Convert.ToString(reader["ReaderFirstName"]),
                ReaderAdress = Convert.ToString(reader["ReaderAdress"]),
                ReaderId = Convert.ToString(reader["ReaderId"]),
                ReaderImage = Convert.ToString(reader["ReaderImage"]),
                ReaderLastName = Convert.ToString(reader["ReaderLastName"]),
                ReaderNickName = Convert.ToString(reader["ReaderNickName"]),
                ReaderPassword = Convert.ToString(reader["ReaderPassword"]),
                ReaderTelephone = Convert.ToString(reader["ReaderTelephone"])

            };
        }
    }
}
