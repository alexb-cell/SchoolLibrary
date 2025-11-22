namespace SchoolLibraryWS
{
    public class Repository
    {
       protected DbHelperOledb helperOledb;
       protected ModelCreators modelCreators;

        public Repository(DbHelperOledb helperOledb,
                          ModelCreators modelCreators)
        {
            this.helperOledb =  helperOledb;
            this.modelCreators = modelCreators;
        }

        

    }
}
