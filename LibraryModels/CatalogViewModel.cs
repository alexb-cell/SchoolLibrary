using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class CatalogViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
        public List<Ganre> Ganres { get; set; }
    }
}
