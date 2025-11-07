namespace SchoolLibraryWS
{
    public class Repository
    {
       protected DbHelperOledb helperOledb;
       protected ModelCreators modelCreators;

        public Repository()
        {
            this.helperOledb = new DbHelperOledb();
            this.modelCreators = new ModelCreators();
        }
    }
}
