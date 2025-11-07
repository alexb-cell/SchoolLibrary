namespace SchoolLibraryWS.ORM.Repositories
{
    public class RepositoryUOW
    {
        AuthoRepository authoRepository;
        BookRepository bookRepository;
        BorrowRepository borrowRepository;
        CityRepository cityRepository;
        CountryRepository countryRepository;
        GanreRepository ganreRepository;
        ReaderRepository readerRepository;

        public AuthoRepository AuthoRepository
        {
            get
            {
                if (this.authoRepository == null)
                    return new AuthoRepository();
                return this.authoRepository;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                    return new BookRepository();
                return this.bookRepository;
            }
        }

        public CountryRepository CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    return new CountryRepository();
                return this.countryRepository;
            }
        }

        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    return new CityRepository();
                return this.cityRepository;
            }
        }

        public GanreRepository GanreRepository
        {
            get
            {
                if (this.ganreRepository == null)
                    return new GanreRepository();
                return this.ganreRepository;
            }
        }

        public ReaderRepository ReaderRepository
        {
            get
            {
                if (this.readerRepository == null)
                    return new ReaderRepository();
                return this.readerRepository;
            }
        }



    }
}
