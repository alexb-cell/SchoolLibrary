using System.Data;

namespace SchoolLibraryWS
{
    public interface IModelCreator<T>
    {
        T CreateModel(IDataReader dataReader);
    }
}
