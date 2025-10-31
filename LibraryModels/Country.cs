using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class Country
    {
        string countryId;
        string countryName;

        public string CountryId
        {
            get { return countryId; }
            set
            {
                countryId = value;

            }
        }

        [Required(ErrorMessage = "City Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "City Name cannot be less than 3 characters.")]
        public string CountryName
        {
            get { return countryName; }
            set
            {
                countryName = value;
            }
        }
    }
}
