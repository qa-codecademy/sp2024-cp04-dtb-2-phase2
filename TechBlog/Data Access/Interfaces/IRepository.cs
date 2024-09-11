using Domain_Models;

namespace Data_Access.Interfaces
{
    public interface IRepository<T> where T : Base
    {
        ICollection<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Any(int id);
        bool Update(T entity);
        bool Delete(T entity);
        bool DeleteById(int id);
    }
}
