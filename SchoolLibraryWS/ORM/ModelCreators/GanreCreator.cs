﻿using LibraryModels;
using System.Data;

namespace SchoolLibraryWS
{
    public class GanreCreator:IModelCreator<Ganre>
    {
        public Ganre CreateModel(IDataReader reader)
        {
            return new Ganre()
            {
                 GanreId= Convert.ToString(reader["GanreId"]),
                 GanreName=Convert.ToString(reader["GanreName"])
            };
        }

        
    }
}
