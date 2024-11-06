using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Implementations
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly TechBlogDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(TechBlogDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public bool Add(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Any(int id) => _table.Any(x => x.Id.Equals(id));

        public bool Delete(T entity)
        {
            _table.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteById(int id)
        {
            var toBeDeleted = GetById(id);
            _table.Remove(toBeDeleted);
            return _context.SaveChanges() > 0;
        }

        public ICollection<T> GetAll() => _table.AsNoTracking().ToList();

        public T? GetById(int id) => _table.FirstOrDefault(x => x.Id.Equals(id));
        public bool Update(T entity)
        {
            _table.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
