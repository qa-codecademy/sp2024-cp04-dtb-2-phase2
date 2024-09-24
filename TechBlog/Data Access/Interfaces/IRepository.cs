using Domain_Models;

namespace Data_Access.Interfaces
{
    public interface IRepository<T> where T : Base
    {
        // methods were set to expect to return bool, were changed to expect nothing 
        // GetAll was changed from ICollection to List
        ICollection<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Any(int id);
        bool Update(T entity);

        //bool Delete(T entity);
        bool DeleteById(int id);
    }
}
