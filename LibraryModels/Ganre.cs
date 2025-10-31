using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class Ganre
    {
        string ganreId;
        string ganreName;

        public Ganre()
        {
           
        }
        public string GanreId
        {
            get { return ganreId; }
            set
            {
                ganreId = value;
            }
        }

        [Required(ErrorMessage = "Ganre Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ganre Name cannot be less than 3 characters.")]
        public string GanreName
        {
            get { return ganreName; }
            set
            {
                ganreName = value;
            }
        }   
    }
}
