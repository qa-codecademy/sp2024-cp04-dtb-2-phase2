using Domain_Models;

namespace Data_Access.Interfaces
{
    public interface IRepository<T> where T : Base
    {
        // methods were set to expect to return bool, were changed to expect nothing 
        // GetAll was changed from ICollection to List
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        bool Any(int id);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);
    }
}
