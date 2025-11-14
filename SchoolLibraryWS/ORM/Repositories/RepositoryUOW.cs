namespace SchoolLibraryWS
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

        DbHelperOledb helperOledb;
        ModelCreators modelCreators;

        public RepositoryUOW()
        {
            this.helperOledb = new DbHelperOledb();
            this.modelCreators = new ModelCreators();
        }

        public DbHelperOledb DbHelperOledb
        {
            get
            {
                return this.helperOledb;
            }
        }

        public AuthoRepository AuthoRepository
        {
            get
            {
                if (this.authoRepository == null)
                    return new AuthoRepository(this.helperOledb, this.modelCreators);
                return this.authoRepository;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                    return new BookRepository(this.helperOledb, this.modelCreators);
                return this.bookRepository;
            }
        }

        public CountryRepository CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    return new CountryRepository(this.helperOledb, this.modelCreators);
                return this.countryRepository;
            }
        }

        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    return new CityRepository(this.helperOledb, this.modelCreators);
                return this.cityRepository;
            }
        }

        public GanreRepository GanreRepository
        {
            get
            {
                if (this.ganreRepository == null)
                    return new GanreRepository(this.helperOledb, this.modelCreators);
                return this.ganreRepository;
            }
        }

        public ReaderRepository ReaderRepository
        {
            get
            {
                if (this.readerRepository == null)
                    return new ReaderRepository(this.helperOledb, this.modelCreators);
                return this.readerRepository;
            }
        }



    }
}
